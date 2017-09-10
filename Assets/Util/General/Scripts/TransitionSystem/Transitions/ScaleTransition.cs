using UnityEngine;
using System.Collections;

public class ScaleTransition : TimedTransition {
    public AnimationCurve curve;

    private Vector3 _originalScale;

    protected void Awake() {
        _originalScale = transform.localScale;
    }

    protected override void updateTransition(float transitionPercent) {
        transform.localScale = _originalScale * curve.Evaluate(transitionPercent);
    }
}
