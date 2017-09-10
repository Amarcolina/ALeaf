using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformUtil {
  public static bool IsAnyChildOf(this Transform transform, GameObject potentialParent) {
    Transform parent = transform.parent;
    while (parent != null) {
      if (parent.gameObject == potentialParent) {
        return true;
      }
      parent = parent.parent;
    }
    return false;
  }

  public static List<Transform> parents(this Transform transform) {
    List<Transform> parents = new List<Transform>();
    Transform parent = transform.parent;
    while (parent != null) {
      parents.Add(parent);
      parent = parent.parent;
    }
    return parents;
  }

  public static void SetGlobalPosX(this Transform transform, float globalX) {
    Vector3 pos = transform.position;
    pos.x = globalX;
    transform.position = pos;
  }

  public static void SetGlobalPosY(this Transform transform, float globalY) {
    Vector3 pos = transform.position;
    pos.y = globalY;
    transform.position = pos;
  }

  public static void SetGlobalPosZ(this Transform transform, float globalZ) {
    Vector3 pos = transform.position;
    pos.z = globalZ;
    transform.position = pos;
  }

  public static void SetLocalPosX(this Transform transform, float localX) {
    Vector3 pos = transform.localPosition;
    pos.x = localX;
    transform.localPosition = pos;
  }

  public static void SetLocalPosY(this Transform transform, float localY) {
    Vector3 pos = transform.localPosition;
    pos.y = localY;
    transform.localPosition = pos;
  }

  public static void SetLocalPosZ(this Transform transform, float localZ) {
    Vector3 pos = transform.localPosition;
    pos.z = localZ;
    transform.localPosition = pos;
  }

  public static void SetLocalScaleX(this Transform transform, float localX) {
    Vector3 scale = transform.localScale;
    scale.x = localX;
    transform.localScale = scale;
  }

  public static void SetLocalScaleY(this Transform transform, float localY) {
    Vector3 scale = transform.localScale;
    scale.y = localY;
    transform.localScale = scale;
  }

  public static void SetLocalScaleZ(this Transform transform, float localZ) {
    Vector3 scale = transform.localScale;
    scale.z = localZ;
    transform.localScale = scale;
  }
}
