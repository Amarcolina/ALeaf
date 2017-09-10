using UnityEngine;
using System.Collections;

public class ChaoticRotator : MonoBehaviour {
    private Vector3 _v1, _v2;

    public void Awake() {
        _v1 = Random.onUnitSphere;
        _v2 = Vector3.Cross(_v1, Random.onUnitSphere);
    }

	void Update () {
        _v1 = Quaternion.AngleAxis(-1.0f, _v2) * _v1;
        _v2 = Quaternion.AngleAxis(1.3f, _v1) * _v2;
        transform.rotation = Quaternion.AngleAxis(5 * Vector3.Dot(_v1, Vector3.up), _v2) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(5 * Vector3.Dot(_v2, Vector3.up), _v1) * transform.rotation;
	}

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + _v1);
        Gizmos.DrawLine(transform.position, transform.position + _v2);
    }
}
