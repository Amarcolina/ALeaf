using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class Color32Interpolator : ValueInterpolatorBase<Color32> {
    public Color32Interpolator(Color32 from, Color32 to, Action<Color32> onValue)
      : base(from, to, onValue) {
      _to = to;
    }

    public override void Interpolate(float progress) {
      _onValue(Color32.Lerp(_from, _to, progress));
    }

    public override float GetLength() {
      return Vector4.Distance((Color)_from, (Color)_to);
    }
  }

}