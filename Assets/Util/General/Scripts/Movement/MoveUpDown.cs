using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Translate(Vector3.up * 0.03f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            transform.Translate(Vector3.down * 0.03f);
        }
	}
}
