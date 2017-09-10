using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class FloatInterpolator : ValueInterpolatorBase<float> {
    public FloatInterpolator(float from, float to, Action<float> onValue)
      : base(from, to, onValue) {
      _to = to - from;
    }

    public override void Interpolate(float progress) {
      _onValue(_from + _to * progress);
    }

    public override float GetLength() {
      return Mathf.Abs(_from - _to);
    }
  }

}