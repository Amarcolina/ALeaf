using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    public bool invertZ = false;

    public void OnWillRenderObject() {
        if (invertZ) {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.current.transform.position);
        } else {
            transform.LookAt(Camera.current.transform);
        }
    }
}
