using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class VectorUtil {

  public static Vector2 xy(this Vector3 v) {
    return new Vector2(v.x, v.y);
  }

  public static Vector2 xz(this Vector3 v) {
    return new Vector2(v.x, v.z);
  }

  public static Vector2 yz(this Vector3 v) {
    return new Vector2(v.y, v.z);
  }

  public static Vector3 randomWithinBounds(this Vector3 v) {
    v.Scale(new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f));
    return v;
  }

  public static Vector4 abs(this Vector4 v) {
    return new Vector4(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z), Mathf.Abs(v.w));
  }

  public static Vector3 abs(this Vector3 v) {
    return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
  }

  public static Vector2 abs(this Vector2 v) {
    return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
  }

  public static float maxc(this Vector4 v) {
    return Mathf.Max(v.x, v.y, v.z, v.w);
  }

  public static float maxc(this Vector3 v) {
    return Mathf.Max(v.x, v.y, v.z);
  }

  public static float maxc(this Vector2 v) {
    return Mathf.Max(v.x, v.y);
  }

  public static float minc(this Vector4 v) {
    return Mathf.Min(v.x, v.y, v.z, v.w);
  }

  public static float minc(this Vector3 v) {
    return Mathf.Min(v.x, v.y, v.z);
  }

  public static float minc(this Vector2 v) {
    return Mathf.Min(v.x, v.y);
  }

  public static Vector3 average(this IEnumerable<Vector3> toAverage) {
    Vector3 sum = Vector3.zero;
    int count = 0;
    foreach (Vector3 v in toAverage) {
      sum += v;
      count++;
    }
    return sum / count;
  }

  public static bool isFinite(this Vector3 v) {
    return !(float.IsInfinity(v.x) || float.IsNaN(v.x) ||
             float.IsInfinity(v.y) || float.IsNaN(v.y) ||
             float.IsInfinity(v.z) || float.IsNaN(v.z));
  }

  public static bool isSmall(this Vector3 v, float maxComponentMagnitude = 1000.0f) {
    return v.isFinite() &&
           Mathf.Abs(v.x) <= maxComponentMagnitude &&
           Mathf.Abs(v.y) <= maxComponentMagnitude &&
           Mathf.Abs(v.z) <= maxComponentMagnitude;
  }
}
