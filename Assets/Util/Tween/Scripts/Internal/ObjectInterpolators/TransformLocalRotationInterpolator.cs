using UnityEngine;
using System.Collections;

namespace TweenInternal {

  public class TransformLocalRotationInterpolator : ObjectInterpolatorBase<Transform, Quaternion, Quaternion> {
    public TransformLocalRotationInterpolator(Transform target, Quaternion from, Quaternion to)
      : base(target, from, to) {
      _to = to;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.localRotation = Quaternion.Slerp(_from, _to, progress);
    }

    public override float GetLength() {
      return Quaternion.Angle(_from, _to);
    }
  }

}