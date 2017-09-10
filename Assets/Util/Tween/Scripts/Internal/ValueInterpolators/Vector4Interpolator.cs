using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class Vector4Interpolator : ValueInterpolatorBase<Vector4> {
    public Vector4Interpolator(Vector4 from, Vector4 to, Action<Vector4> onValue)
      : base(from, to, onValue) {
      _to = to - from;
    }

    public override void Interpolate(float progress) {
      _onValue(_from + _to * progress);
    }

    public override float GetLength() {
      return Vector4.Distance(_from, _to);
    }
  }

}