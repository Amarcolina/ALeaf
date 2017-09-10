using UnityEngine;
using System.Collections;

public class LightScaler : MonoBehaviour {

    void Start() {
        GetComponent<Light>().range *= transform.parent.localScale.x;
    }
}
