using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class MaterialColorInterpolator : ObjectInterpolatorBase<Material, Color, Color> {
    private string _propertyName;

    public MaterialColorInterpolator(Material target, string propertyName, Color from, Color to)
      : base(target, from, to) {
      _propertyName = propertyName;
      _to = to - from;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.SetColor(_propertyName, _from + _to * progress);
    }

    public override float GetLength() {
      return Vector4.Distance(_from, _to);
    }
  }

}