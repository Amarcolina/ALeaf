using UnityEngine;
using System.Collections;

public class FadeTransition : TimedTransition {
    public string materialProperty = "_Fade";
    public AnimationCurve curve;

    protected override void updateTransition(float transitionPercent) {
        GetComponent<Renderer>().material.SetFloat(materialProperty, curve.Evaluate(transitionPercent));
    }
}
