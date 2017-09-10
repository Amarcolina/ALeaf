using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class QuaternionUtil {
    public static Quaternion average(this IEnumerable<Quaternion> quats) {
        Quaternion avg = new Quaternion(0, 0, 0, 0);
        Quaternion first = Quaternion.identity;
        bool hasFirst = false;

        foreach (Quaternion q in quats) {
            if (!hasFirst) {
                first = q;
            }

            if (Quaternion.Dot(q, avg) > 0) {
                avg.x += q.x;
                avg.y += q.y;
                avg.z += q.z;
                avg.w += q.w;
            } else {
                avg.x += -q.x;
                avg.y += -q.y;
                avg.z += -q.z;
                avg.w += -q.w;
            }
        }

        var mag = Mathf.Sqrt(avg.x * avg.x + avg.y * avg.y + avg.z * avg.z + avg.w * avg.w);

        if (mag > 0.0001) {
            avg.x /= mag;
            avg.y /= mag;
            avg.z /= mag;
            avg.w /= mag;
        } else {
            avg = first;
        }
        return avg;
    }
}
