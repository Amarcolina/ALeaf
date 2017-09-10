using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    [Range (-360, 360)]
    public float speed = 10.0f;
	
	void Update () {
        Vector3 euler = transform.localEulerAngles;
        euler.y += speed * Time.deltaTime;
        transform.localEulerAngles = euler;
	}
}
