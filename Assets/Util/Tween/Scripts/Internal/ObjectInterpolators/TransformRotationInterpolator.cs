using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class TransformRotationInterpolator : ObjectInterpolatorBase<Transform, Quaternion, Quaternion> {
    public TransformRotationInterpolator(Transform target, Quaternion from, Quaternion to)
      : base(target, from, to) {
      _to = to;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.rotation = Quaternion.Slerp(_from, _to, progress);
    }

    public override float GetLength() {
      return Quaternion.Angle(_from, _to);
    }
  }

}