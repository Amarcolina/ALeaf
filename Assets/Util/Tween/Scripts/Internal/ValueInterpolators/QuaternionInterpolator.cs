using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class QuaternionInterpolator : ValueInterpolatorBase<Quaternion> {
    public QuaternionInterpolator(Quaternion from, Quaternion to, Action<Quaternion> onValue) : base(from, to, onValue) { }

    public override void Interpolate(float progress) {
      _onValue(Quaternion.Slerp(_from, _to, progress));
    }

    public override float GetLength() {
      return Quaternion.Angle(_from, _to);
    }
  }

}