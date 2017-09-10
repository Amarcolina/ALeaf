using UnityEngine;
using System.Collections;

public class PersistantCircleEffect : MonoBehaviour {
    public AnimationCurve circle1Lerp = null;
    public MeshRenderer circle1;
    public AnimationCurve circle2Lerp = null;
    public MeshRenderer circle2;

    private MeshRenderer[] renderers;

    [MinValue(0)]
    public float animationInTime = 1.0f;

    [MinValue(0)]
    public float animationOutTime = 1.0f;

    private bool isPinched;

    private float animationPercent = 0;
    private bool wasUpdated = false;

    private bool disconnected = false;

    private AudioListener source;
    private Vector3 lookLocation;

    public void Awake() {
        wasUpdated = true;
        source = FindObjectOfType<AudioListener>();
    }

    public void LateUpdate() {
        if (wasUpdated == false) {
            disconnected = true;
        }

        bool isIn = isPinched && !disconnected;
        animationPercent = Mathf.MoveTowards(animationPercent, isIn ? 1.0f : 0.0f, Time.deltaTime / (isIn ? animationInTime : animationOutTime));

        circle1.material.SetFloat("_Radius", circle1Lerp.Evaluate(animationPercent));
        circle2.material.SetFloat("_Radius", circle2Lerp.Evaluate(animationPercent));

        transform.LookAt(lookLocation);

        if (disconnected && animationPercent == 0.0f) {
            Destroy(gameObject);
        }

        wasUpdated = false;
    }

    public void setPinchedStatus(Vector3 location, bool isPinched) {
        this.isPinched = isPinched;
        transform.position = location;
        lookLocation = source.transform.position;
        wasUpdated = true;
    }

    public void setPinchedStatus(Vector3 location, Vector3 lookLocation, bool isPinched) {
        this.isPinched = isPinched;
        transform.position = location;
        this.lookLocation = lookLocation;
        wasUpdated = true;
    }
}
