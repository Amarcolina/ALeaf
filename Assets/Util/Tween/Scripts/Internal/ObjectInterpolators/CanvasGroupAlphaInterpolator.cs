using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class CanvasGroupAlphaInterpolator : ObjectInterpolatorBase<CanvasGroup, float, float> {
    public CanvasGroupAlphaInterpolator(CanvasGroup target, float from, float to)
      : base(target, from, to) {
      _to = to;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.alpha = Mathf.Lerp(_from, _to, progress);
    }

    public override float GetLength() {
      return Mathf.Abs(_from - _to);
    }
  }

}