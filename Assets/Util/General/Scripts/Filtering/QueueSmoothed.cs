using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

public class QueueSmoothed : MonoBehaviour {
    
    [MinValue(1)]
    public int smoothingLength = 15;

    private Queue<Vector3> posQueue = new Queue<Vector3>();
    private Queue<Quaternion> rotQueue = new Queue<Quaternion>();

    void Start() {
        reset();
    }

    public void reset() {
        posQueue.Clear();
        rotQueue.Clear();
        for (int i = 0; i < smoothingLength; i++) {
            posQueue.Enqueue(transform.parent.position);
            rotQueue.Enqueue(transform.parent.rotation);
        }
    }

#if UNITY_EDITOR

    [ContextMenu ("Assign settings to all")]
    void assignThisSmoothingToAll() {
        int mySmoothing = smoothingLength;
        foreach (QueueSmoothed s in FindObjectsOfType<QueueSmoothed>()) {
            s.smoothingLength = mySmoothing;
            EditorUtility.SetDirty(s);
        }
    }

    [MenuItem("Tools/Create Smoothed Object From Selected")]
    static void createSmoothedObject() {
        GameObject selected = Selection.activeGameObject;

        Undo.RecordObject(selected, "Created smoothing");

        Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers){
            if (renderer.GetComponentsInChildren<Transform>().Length != 1) {
                Debug.LogWarning("Could not smooth " + renderer.gameObject + " because it has children");
                continue;
            }

            GameObject smoothedObj = Instantiate(renderer.gameObject) as GameObject;
            Undo.RegisterCreatedObjectUndo(smoothedObj, "Created obj");

            smoothedObj.transform.parent = renderer.gameObject.transform;
            smoothedObj.transform.localScale = Vector3.one;
            smoothedObj.transform.localPosition = Vector3.zero;
            smoothedObj.transform.localRotation = Quaternion.identity;
            smoothedObj.name = "Smoothed" + renderer.gameObject.name;

            foreach (Component c in smoothedObj.GetComponents<Component>()) {
                if (c is Transform || c is Renderer || c is MeshFilter) {
                    continue;
                }
                Undo.DestroyObjectImmediate(c);
            }

            smoothedObj.AddComponent<QueueSmoothed>();

            foreach (Component c in renderer.gameObject.GetComponents<Component>()) {
                if (c is Renderer || c is MeshFilter) {
                    Undo.DestroyObjectImmediate(c);
                }
            }
        }
    }
#endif

	void LateUpdate () {
        posQueue.Enqueue(transform.parent.position);
        rotQueue.Enqueue(transform.parent.rotation);

        if (posQueue.Count > smoothingLength) {
            posQueue.Dequeue();
            rotQueue.Dequeue();
        }

        transform.position = posQueue.average();
        transform.rotation = rotQueue.average();
	}
}
