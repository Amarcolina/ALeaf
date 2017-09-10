using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class MaterialValueInterpolator : ObjectInterpolatorBase<Material, float, float> {
    private string _propertyName;

    public MaterialValueInterpolator(Material target, string propertyName, float from, float to)
      : base(target, from, to) {
      _propertyName = propertyName;
      _to = to - from;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.SetFloat(_propertyName, _from + _to * progress);
    }

    public override float GetLength() {
      return Mathf.Abs(_from - _to);
    }
  }

}