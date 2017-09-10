using UnityEngine;
using System.Collections;

public interface IVelocityFilter {
    void reportPosition(Vector3 position, float time);
    Vector3 getVelocity();
}
