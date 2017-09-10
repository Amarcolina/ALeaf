using UnityEngine;
using System.Collections;

public class SpringAnchor : MonoBehaviour {
    public float constant = 100.0f;

    private Rigidbody _rigidbody;
    private Vector3 _anchor;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _anchor = transform.position;
    }

    void Update() {
        Vector3 delta = _anchor - transform.position;
        _rigidbody.AddForce(delta * constant);
    }
}
