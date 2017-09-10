using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class TransformPositionInterpolator : ObjectInterpolatorBase<Transform, Vector3, Vector3> {
    public TransformPositionInterpolator(Transform target, Vector3 from, Vector3 to)
      : base(target, from, to) {
      _to = to - from;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.position = _from + _to * progress;
    }

    public override float GetLength() {
      return _to.magnitude;
    }
  }

}