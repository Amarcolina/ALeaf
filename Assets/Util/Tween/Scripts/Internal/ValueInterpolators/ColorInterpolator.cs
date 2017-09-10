using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class ColorInterpolator : ValueInterpolatorBase<Color> {
    public ColorInterpolator(Color from, Color to, Action<Color> onValue)
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