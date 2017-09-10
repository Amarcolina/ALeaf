using UnityEngine;
using System.Collections;

public class EnableComponentAfterDelay : MonoBehaviour {
    public MonoBehaviour component;

    
    [MinValue(0)]
    public float delay = 0.0f;

    void Start() {
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(delayCoroutine());
    }

    private IEnumerator delayCoroutine() {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
