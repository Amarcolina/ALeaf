using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EnableDepthTexture : MonoBehaviour {
  public DepthTextureMode depthTextureMode = DepthTextureMode.Depth;

  void Awake() {
    Camera c = GetComponent<Camera>();
    if (c != null) {
      c.depthTextureMode = depthTextureMode;
    }
  }

  void OnDestroy() {
    if (gameObject != null) {
      Camera c = GetComponent<Camera>();
      if (c != null) {
        c.depthTextureMode = DepthTextureMode.None;
      }
    }
  }
}
