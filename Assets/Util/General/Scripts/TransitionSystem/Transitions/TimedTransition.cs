using UnityEngine;
using System.Linq;
using System.Collections;

public abstract class TimedTransition : Transition {
    
    [MinValue(0)]
    public float transitionTime;

    private float _transitionPercent = 0.0f;
    private float _destinationPercent = 1.0f;

    public void Update() {
        _transitionPercent = Mathf.MoveTowards(_transitionPercent, _destinationPercent, Time.deltaTime / transitionTime);
        if (_transitionPercent == 1.0f) {
            enabled = false;
        }
        if (_transitionPercent == 0.0f) {
            reportTransitionFinished();
            enabled = false;
        }
        updateTransition(_transitionPercent);
    }

    public override void beginTransitionOut() {
        _destinationPercent = 0.0f;
        enabled = true;
    }

    protected abstract void updateTransition(float transitionPercent);

}
