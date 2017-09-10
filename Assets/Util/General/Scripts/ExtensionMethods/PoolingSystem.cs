using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pooling {

  /* This is a lightweight pooling system used for situations where many objects of the same type
   * are frequently getting instantiated or destroyed.  Memory and CPU efficiency can be attained
   * by reusing objects instead of destroying them.  This class adds extension methods to the 
   * GameObject class to allow easy spawning and recycling of objects.  To create a new instance
   * of a prefab, simply call prefab.spawn().  The pooling system will either instantiate the prefab
   * like normal, or grab a previously recycled object and re-use it.  For this system to work,
   * objects must be explicitly recycled instead of destroyed.  Simply call gameObject.recycle() 
   * to recycle an instance to allow it to be re-used by the pooling system.
   */

  public static class PoolingSystem {
    public const int DEFAULT_POOL_CAPACITY = int.MaxValue;
    public const int DEFAULT_INSTANCE_CAPACITY = int.MaxValue;

    /* Clears all pools across all prefabs 
     */
    public static void clearAllPools() {
      foreach (Pool pool in _objectPools.Values) {
        pool.setPoolSize(0);
      }
    }

    /* Gets the total number of pooled objects across all prefabs
     */
    public static int getTotalPooledCount() {
      int size = 0;
      foreach (Pool pool in _objectPools.Values) {
        size += pool.getPoolSize();
      }
      return size;
    }

    /* Gets the total number of instances that are active in the scene
        */
    public static int getTotalInstanceCount() {
      return _activeObjects.Count;
    }

    /* Spawns a new object based off of a prefab.  This method draws from the object pool if it contains
     * any objects, otherwise is instantiates new a new object. If the maximum instance count has already been 
     * reached, the behavior is based on the 'forceSpawn' bool.  
     *      If true, the oldest existing instance is recycled into the new instance.
     *      If false, no action is taken.
     * 
     * This method returns the object spawned, or null if none were spawned
     */
    public static GameObject spawnInstance(this GameObject prefab, bool forceSpawn = true) {
      return getPoolAuto(prefab).spawn(Vector3.zero, Quaternion.identity, forceSpawn, false);
    }

    public static GameObject spawnInstance(this GameObject prefab, Vector3 position, Quaternion rotation, bool forceSpawn = true) {
      return getPoolAuto(prefab).spawn(position, rotation, forceSpawn, true);
    }

    /* Recycles an object and allows it to be returned to the object pool.  If the pool is full
     * this will destroy the object immidiately.
     * 
     * This method is safe to call even if your object is not an object instantiated via spawn.
     * If it was not instantiated via spawn this method behaves exactly the same as Destroy()
     */
    public static void recycle(this GameObject instance) {
      Pool pool;
      if (_activeObjects.TryGetValue(instance, out pool)) {
        _activeObjects.Remove(instance);
        pool.recycle(instance);
      } else {
        GameObject.Destroy(instance);
      }
    }

    /* Sets the pool size for a given prefab.  If the current pool is smaller than the given size,
     * this method will instatiate new objects to fill the pool.  If the pool is larger than 
     * the given size, this method will destroy objects to shrink the pool.
     */
    public static void setPoolSize(this GameObject prefab, int poolSize) {
      if (poolSize < 1) {
        throw new ArgumentOutOfRangeException("poolSize", "Pool size was " + poolSize + ", but needs to be greater than 0");
      }
      getPoolAuto(prefab).setPoolSize(poolSize);
    }

    /* Gets the size of the object pool for this prefab.  This can sometimes be larger than
     * the maximum size if the max size was decreased while there were many objects in the pool
     */
    public static int getPoolSize(this GameObject prefab) {
      Pool pool;
      if (_objectPools.TryGetValue(prefab, out pool)) {
        return pool.getPoolSize();
      }
      return 0;
    }

    /* A simple util to grow the pool for a given prefab by a certain amount
     */
    public static void growPoolBy(this GameObject prefab, int growthSize) {
      prefab.setPoolSize(prefab.getPoolSize() + growthSize);
    }

    /* Sets the maximum size a pool can grow to.  By default pools are unlimited in size.  If the
     * capacity is less than the current pool size, and 'destroyExtra' is true, the objects in
     * the pool will be destroyed to shrink the pool to the max size
     */
    public static void setPoolCapacity(this GameObject prefab, int capacity, bool destroyExtra = false) {
      if (capacity < 1) {
        throw new ArgumentOutOfRangeException("capacity", "Capacity size was " + capacity + ", but needs to be greater than 0");
      }
      getPoolAuto(prefab).setPoolCapacity(capacity, destroyExtra);
    }

    /* Gets the maximum size the pool can grow for this prefab
     */
    public static int getPoolCapacity(this GameObject prefab) {
      Pool pool;
      if (_objectPools.TryGetValue(prefab, out pool)) {
        return pool.getPoolCapacity();
      }
      return DEFAULT_POOL_CAPACITY;
    }

    /* Sets the maximum number of instances that can be instantiated from this prefab.  If the
     * given amount is less than the current number of instances, and 'recycleExtra' is true,
     * extra instanced objects will be recycled to shrink the instance count to the maximum
     */
    public static void setInstanceCapacity(this GameObject prefab, int capacity, bool recycleExtra = false) {
      if (capacity < 1) {
        throw new ArgumentOutOfRangeException("capacity", "Capacity was " + capacity + ", but needs to be greater than 0");
      }
      getPoolAuto(prefab).setInstanceCapacity(capacity, recycleExtra);
    }

    /* Gets the maximum number of instances that can be instantiated from this prefab.
     */
    public static int getInstanceCapacity(this GameObject prefab) {
      Pool pool;
      if (_objectPools.TryGetValue(prefab, out pool)) {
        return pool.getInstanceCapacity();
      }
      return DEFAULT_INSTANCE_CAPACITY;
    }

    /* Gets the current number of instances that have been spawned from this prefab.  This number
     * can sometimes be larger than the maximum instance count if the max count was decreased
     * while there were many instances
     */
    public static int getInstanceCount(this GameObject prefab) {
      Pool pool;
      if (_objectPools.TryGetValue(prefab, out pool)) {
        return pool.getInstanceCount();
      }
      return 0;
    }

    /* Recycles instances until there are only a certain number of instances remaining
     */
    public static int recycleInstancesDownTo(this GameObject prefab, int count) {
      if (count < 0) {
        throw new ArgumentOutOfRangeException("count", "Canot recycle down to " + count + " instances, the amount needs to be positive");
      }
      return getPoolAuto(prefab).recycleInstancesDownTo(count);
    }

    //Maps a prefab to the pool connected to that prefab.
    private static Dictionary<GameObject, Pool> _objectPools = new Dictionary<GameObject, Pool>();

    //Maps an instantiated object to the pool it belongs to.
    private static Dictionary<GameObject, Pool> _activeObjects = new Dictionary<GameObject, Pool>();

    private class Pool {
      public readonly GameObject prefab;

      private int _poolCapacity = DEFAULT_POOL_CAPACITY;
      private int _instanceCapacity = DEFAULT_INSTANCE_CAPACITY;

      private Queue<GameObject> _pooledObjects = new Queue<GameObject>();
      private HashSet<GameObject> _instanceSet = new HashSet<GameObject>();

      public Pool(GameObject prefab) {
        this.prefab = prefab;
      }

      public GameObject spawn(Vector3 position, Quaternion rotation, bool forceSpawn, bool updateTransform) {
        GameObject spawned = null;

        //If we have reached the limit of how many objects can be instantiated at once
        if (_instanceSet.Count >= _instanceCapacity) {
          if (forceSpawn) {
            //Force the oldest instance to be recycled to make room for the new one
            tryRecycle();
          } else {
            //Fail to spawn object
            return null;
          }
        }

        //Spawn object from the pool
        while (_pooledObjects.Count != 0) {
          spawned = _pooledObjects.Dequeue();

          //Spawned could potentially be null if it used Destroy() instead of Recycle()
          if (spawned != null) {
            if (updateTransform) {
              spawned.transform.position = position;
              spawned.transform.rotation = rotation;
            } else {
              spawned.transform.position = prefab.transform.position;
              spawned.transform.rotation = prefab.transform.rotation;
            }
            break;
          }
        }

        //If an object could not be spawned from the pool, instantiate it instead
        if (spawned == null) {
          if (updateTransform) {
            spawned = GameObject.Instantiate(prefab, position, rotation) as GameObject;
          } else {
            spawned = GameObject.Instantiate(prefab) as GameObject;
          }
        }

        spawned.SetActive(true);

        _activeObjects[spawned] = this;
        _instanceSet.Add(spawned);

        return spawned;
      }

      private bool isNull(GameObject obj) {
        return obj == null;
      }

      /* Helper method that recycles the oldest instance object.  This method returns 
       * true if it was able to recycle an object and false otherwise
       */
      private bool tryRecycle() {
        _instanceSet.RemoveWhere(isNull);
        if (_instanceSet.Count == 0) {
          return false;
        }

        var enumerator = _instanceSet.GetEnumerator();
        enumerator.MoveNext();
        GameObject toRecycle = enumerator.Current;
        toRecycle.recycle();
        return true;
      }

      public void recycle(GameObject obj) {
        _instanceSet.Remove(obj);
        //Use >= because the pool count can sometimes be larger than the pool capacity
        if (_pooledObjects.Count >= _poolCapacity) {
          GameObject.Destroy(obj);
        } else {
          _pooledObjects.Enqueue(obj);
          obj.SetActive(false);
        }
      }

      public int getInstanceCount() {
        return _instanceSet.Count;
      }

      public int recycleInstancesDownTo(int count) {
        int instancesRecycled = 0;
        while (_instanceSet.Count > count) {
          if (tryRecycle()) {
            instancesRecycled++;
          } else {
            break;
          }
        }
        return instancesRecycled;
      }

      public int getPoolSize() {
        return _pooledObjects.Count;
      }

      public void setPoolSize(int poolSize) {
        while (_pooledObjects.Count < poolSize) {
          GameObject spawned = GameObject.Instantiate(prefab) as GameObject;
          spawned.SetActive(false);
          _pooledObjects.Enqueue(spawned);
        }

        while (_pooledObjects.Count > poolSize) {
          GameObject.Destroy(_pooledObjects.Dequeue());
        }
      }

      public int getPoolCapacity() {
        return _poolCapacity;
      }

      public void setPoolCapacity(int poolCapacity, bool destroyExtra) {
        _poolCapacity = poolCapacity;
        if (destroyExtra && _poolCapacity > _pooledObjects.Count) {
          setPoolSize(_poolCapacity);
        }
      }

      public int getInstanceCapacity() {
        return _instanceCapacity;
      }

      public void setInstanceCapacity(int instanceCapacity, bool recycleExtra) {
        _instanceCapacity = instanceCapacity;
        if (recycleExtra) {
          recycleInstancesDownTo(_instanceCapacity);
        }
      }
    }

    private static Pool getPoolAuto(GameObject prefab) {
      Pool pool;
      if (!_objectPools.TryGetValue(prefab, out pool)) {
        pool = new Pool(prefab);
        _objectPools[prefab] = pool;
      }
      return pool;
    }
  }

}