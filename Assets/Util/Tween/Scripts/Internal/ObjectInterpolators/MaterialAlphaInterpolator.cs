using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class MaterialAlphaInterpolator : ObjectInterpolatorBase<Material, float, float> {
    private string _propertyName;

    public MaterialAlphaInterpolator(Material target, string propertyName, float from, float to)
      : base(target, from, to) {
      _propertyName = propertyName;
      _to = to - from;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.SetColor(_propertyName, _target.GetColor(_propertyName).atAlpha(_from + _to * progress));
    }

    public override float GetLength() {
      return Mathf.Abs(_from - _to);
    }
  }

}