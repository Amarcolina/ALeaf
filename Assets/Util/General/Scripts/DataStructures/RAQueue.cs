using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Generic Queue data structure that allows indexing.  C# default Queue data
 * structure does not :(
 */
public class RAQueue<T> : IEnumerable<T>, IEnumerable {
  public const int DEFAULT_STARTING_CAPACITY = 16;

  private T[] _array;
  private int _count = 0;
  private int _head = 0;
  private int _tail = 0;

  public RAQueue() {
    _array = new T[DEFAULT_STARTING_CAPACITY];
  }

  public RAQueue(int capacity) {
    _array = new T[capacity];
  }

  public void Enqueue(T t) {
    //If adding one would surpass the array capacity
    if (_count == _array.Length) {
      expandArray();
    }

    _array[_head] = t;
    _head = (_head + 1) % _array.Length;
    _count++;
  }

  public int Capacity {
    get {
      return _array.Length;
    }
  }

  public int Count {
    get {
      return _count;
    }
  }

  public T Dequeue() {
    if (_count == 0) {
      throw new System.InvalidOperationException("The queue is empty!");
    }

    T ret = _array[_tail];
    _tail = (_tail + 1) % _array.Length;
    _count--;
    return ret;
  }

  public T PeekTail() {
    if (_count == 0) {
      throw new System.InvalidOperationException("The queue is empty!");
    }
    return _array[_tail];
  }

  public T PeekHead() {
    if (_count == 0) {
      throw new System.InvalidOperationException("The queue is empty!");
    }

    return _array[(_head + - 1 + _array.Length) % _array.Length];
  }

  public void Clear() {
    //Clear array to remove references
    for (int i = 0; i < _count; i++) {
      this[i] = default(T);
    }

    _count = 0;
    _head = 0;
    _tail = 0;
  }

  public bool Contains(T t) {
    for (int i = 0; i < _count; i++) {
      if (t.Equals(this[i])) {
        return true;
      }
    }
    return false;
  }

  public T this[int index] {
    get {
      return _array[getInternalIndex(index)];
    }
    set {
      _array[getInternalIndex(index)] = value;
    }
  }

  public IEnumerator<T> GetEnumerator() {
    return getInternalEnumerator();
  }

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    return getInternalEnumerator();
  }

  private IEnumerator<T> getInternalEnumerator() {
    for (int i = 0; i < _count; i++) {
      yield return this[i];
    }
  }

  private int getInternalIndex(int index) {
    if (index < 0 || index >= _count) {
      throw new System.IndexOutOfRangeException("The specified index " + index + " was out of range!");
    }
    return (index + _tail) % _array.Length;
  }

  private void expandArray() {
    T[] newArray = new T[_array.Length * 2];
    for (int i = 0; i < _count; i++) {
      newArray[i] = this[i];
    }

    _array = newArray;
    _tail = 0;
    _head = _count;
  }
}