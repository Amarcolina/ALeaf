using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class Vector2Interpolator : ValueInterpolatorBase<Vector2> {
    public Vector2Interpolator(Vector2 from, Vector2 to, Action<Vector2> onValue)
      : base(from, to, onValue) {
      _to = to - from;
    }

    public override void Interpolate(float progress) {
      _onValue(_from + _to * progress);
    }

    public override float GetLength() {
      return Vector2.Distance(_from, _to);
    }
  }

}