using UnityEngine;
using System.Collections;

public class GeometryUtil {
  private const float EPSILON = 0.0000001f;
  public static float distanceBetweenSegments(Vector3 s1p0, Vector3 s1p1, Vector3 s2p0, Vector3 s2p1) {
    Vector3 u = s1p1 - s1p0;
    Vector3 v = s2p1 - s2p0;
    Vector3 w = s1p0 - s2p0;
    float a = Vector3.Dot(u, u);
    float b = Vector3.Dot(u, v);
    float c = Vector3.Dot(v, v);
    float d = Vector3.Dot(u, w);
    float e = Vector3.Dot(v, w);
    float D = a * c - b * b;
    float sc, sN, sD = D;
    float tc, tN, tD = D;

    if (D < EPSILON) {
      sN = 0.0f;
      sD = 1.0f;
      tN = e;
      tD = c;
    } else {
      sN = (b * e - c * d);
      tN = (a * e - b * d);
      if (sN < 0.0f) {        // sc < 0 => the s=0 edge is visible
        sN = 0.0f;
        tN = e;
        tD = c;
      } else if (sN > sD) {  // sc > 1  => the s=1 edge is visible
        sN = sD;
        tN = e + b;
        tD = c;
      }
    }

    if (tN < 0.0f) {            // tc < 0 => the t=0 edge is visible
      tN = 0.0f;
      // recompute sc for this edge
      if (-d < 0.0)
        sN = 0.0f;
      else if (-d > a)
        sN = sD;
      else {
        sN = -d;
        sD = a;
      }
    } else if (tN > tD) {      // tc > 1  => the t=1 edge is visible
      tN = tD;
      // recompute sc for this edge
      if ((-d + b) < 0.0f)
        sN = 0.0f;
      else if ((-d + b) > a)
        sN = sD;
      else {
        sN = (-d + b);
        sD = a;
      }
    }
    // finally do the division to get sc and tc
    sc = (Mathf.Abs(sN) < EPSILON ? 0.0f : sN / sD);
    tc = (Mathf.Abs(tN) < EPSILON ? 0.0f : tN / tD);

    // get the difference of the two closest points
    Vector3 dP = w + (sc * u) - (tc * v);  // =  S1(sc) - S2(tc)

    return dP.magnitude;   // return the closest distance
  }

  public static float areaOfTriangle(Vector3 p0, Vector3 p1, Vector3 p2) {
    return Vector3.Cross(p0 - p1, p0 - p2).magnitude / 2.0f;
  }
}
