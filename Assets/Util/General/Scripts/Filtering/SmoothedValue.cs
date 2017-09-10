using UnityEngine;
using System.Collections;

public class SmoothedValue {
    private float _transition = 0.0f;
    private float _transitionTime;

    public SmoothedValue(float transitionTime) {
        _transitionTime = transitionTime;
    }

    public void step() {
        _transition = Mathf.MoveTowards(_transition, 1.0f, Time.deltaTime / _transitionTime);
    }

    public void pushBack(float percent){
        _transition = Mathf.Min(percent, _transition);
    }

    public bool finished() {
        return _transition == 1.0f;
    }

    public void finish() {
        _transition = 1.0f;
    }

    public static implicit operator float(SmoothedValue t) {
        return Mathf.SmoothStep(0.0f, 1.0f, t._transition);
    }
}
