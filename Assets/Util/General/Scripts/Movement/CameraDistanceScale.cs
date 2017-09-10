using UnityEngine;
using System.Collections;

public class CameraDistanceScale : MonoBehaviour {
    public Color tintColor;
    public float farScale = 2.0f;

    public float maxHeight = 2;

    private Renderer _renderer;

    public void Update() {
        AudioListener l = FindObjectOfType<AudioListener>();

        float percent = Mathf.Tan(farScale) * Vector3.Distance(transform.position, l.transform.position);
        transform.localScale = Vector3.one * percent;
        transform.LookAt(l.transform);

        if (_renderer == null) {
            _renderer = GetComponent<Renderer>();
        }

        tintColor.a = 0.5f * (1.0f - Mathf.Clamp01(transform.position.y / maxHeight));
        if (Application.isPlaying) {
            _renderer.material.SetColor("_TintColor", tintColor);
        }
    }
}
