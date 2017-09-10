using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class Vector3Interpolator : ValueInterpolatorBase<Vector3> {
    public Vector3Interpolator(Vector3 from, Vector3 to, Action<Vector3> onValue)
      : base(from, to, onValue) {
      _to = to - from;
    }

    public override void Interpolate(float progress) {
      _onValue(_from + _to * progress);
    }

    public override float GetLength() {
      return Vector3.Distance(_from, _to);
    }
  }

}