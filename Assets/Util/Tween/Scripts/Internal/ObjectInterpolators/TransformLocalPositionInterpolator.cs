using UnityEngine;
using System.Collections;

namespace TweenInternal {

  public class TransformLocalPositionInterpolator : ObjectInterpolatorBase<Transform, Vector3, Vector3> {
    public TransformLocalPositionInterpolator(Transform target, Vector3 from, Vector3 to) :
      base(target, from, to) {
      _to = to - from;
    }

    public override bool IsValid() {
      return _target != null;
    }

    public override void Interpolate(float progress) {
      _target.localPosition = _from + _to * progress;
    }

    public override float GetLength() {
      return _to.magnitude;
    }
  }

}