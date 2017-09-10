using UnityEngine;
using System.Collections;

public class ThresholdTune : MonoBehaviour {
  public static KeyCode incrementThreshold = KeyCode.Equals;
  public static KeyCode decrementThreshold = KeyCode.Minus;

  void Update() {
    if (Input.GetKeyDown(incrementThreshold)) {
      updateMat(0.02f);
    }
    if (Input.GetKeyDown(decrementThreshold)) {
      updateMat(-0.02f);
    }
  }

  private void updateMat(float delta) {
    Renderer r = GetComponent<Renderer>();

    float min = r.material.GetFloat("_MinThreshold");
    float max = r.material.GetFloat("_MaxThreshold");
    float center = (min + max) / 2.0f;
    float spread = Mathf.Abs(min - max) / 2.0f;

    center += delta;

    r.material.SetFloat("_MinThreshold", center + spread);
    r.material.SetFloat("_MaxThreshold", center - spread);
  }
}
