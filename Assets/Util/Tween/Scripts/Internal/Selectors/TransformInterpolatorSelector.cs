using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class TransformInterpolatorSelector {
    private Transform _target;
    private TweenObj _obj;

    public TransformInterpolatorSelector(TweenObj obj, Transform target) {
      _target = target;
      _obj = obj;
    }

    #region GLOBAL
    //GLOBAL TRANSFORM
    public ITweenObj Transform(Transform from, Transform to) {
      _obj.AddInterpolator(new TransformPositionInterpolator(_target, from.position, to.position));
      _obj.AddInterpolator(new TransformRotationInterpolator(_target, from.rotation, to.rotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, to.localScale));
      return _obj;
    }

    public ITweenObj To(Transform to) {
      _obj.AddInterpolator(new TransformPositionInterpolator(_target, _target.position, to.position));
      _obj.AddInterpolator(new TransformRotationInterpolator(_target, _target.rotation, to.rotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, to.localScale));
      return _obj;
    }

    public ITweenObj To(Vector3 toPosition, Quaternion toRotation, Vector3 toLocalScale) {
      _obj.AddInterpolator(new TransformPositionInterpolator(_target, _target.position, toPosition));
      _obj.AddInterpolator(new TransformRotationInterpolator(_target, _target.rotation, toRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, toLocalScale));
      return _obj;
    }

    public ITweenObj From(Transform from) {
      _obj.AddInterpolator(new TransformPositionInterpolator(_target, from.position, _target.position));
      _obj.AddInterpolator(new TransformRotationInterpolator(_target, from.rotation, _target.rotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, _target.localScale));
      return _obj;
    }

    public ITweenObj From(Vector3 fromPosition, Quaternion fromRotation, Vector3 fromLocalScale) {
      _obj.AddInterpolator(new TransformPositionInterpolator(_target, fromPosition, _target.position));
      _obj.AddInterpolator(new TransformRotationInterpolator(_target, fromRotation, _target.rotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, fromLocalScale, _target.localScale));
      return _obj;
    }

    //GLOBAL POSITION
    public ITweenObj Position(Transform from, Transform to) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, from.position, to.position));
    }

    public ITweenObj Position(Vector3 from, Vector3 to) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, from, to));
    }

    public ITweenObj ToPosition(Transform to) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, _target.position, to.position));
    }

    public ITweenObj ToPosition(Vector3 to) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, _target.position, to));
    }

    public ITweenObj FromPosition(Transform from) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, from.position, _target.position));
    }

    public ITweenObj FromPosition(Vector3 from) {
      return _obj.AddInterpolator(new TransformPositionInterpolator(_target, from, _target.position));
    }

    //GLOBAL ROTATION
    public ITweenObj Rotation(Transform from, Transform to) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, from.rotation, to.rotation));
    }

    public ITweenObj Rotation(Quaternion from, Quaternion to) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, from, to));
    }

    public ITweenObj ToRotation(Transform to) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, _target.rotation, to.rotation));
    }

    public ITweenObj ToRotation(Quaternion to) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, _target.rotation, to));
    }

    public ITweenObj FromRotation(Transform from) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, from.rotation, _target.rotation));
    }

    public ITweenObj FromRotation(Quaternion from) {
      return _obj.AddInterpolator(new TransformRotationInterpolator(_target, from, _target.rotation));
    }

    #endregion
    //LOCAL TRANSFORM
    public ITweenObj LocalTransform(Transform from, Transform to) {
      _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from.localPosition, to.localPosition));
      _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from.localRotation, to.localRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, to.localScale));
      return _obj;
    }

    public ITweenObj ToLocal(Transform to) {
      _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, _target.localPosition, to.localPosition));
      _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, _target.localRotation, to.localRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, to.localScale));
      return _obj;
    }

    public ITweenObj ToLocal(Vector3 toLocalPosition, Quaternion toLocalRotation, Vector3 toLocalScale) {
      _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, _target.localPosition, toLocalPosition));
      _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, _target.localRotation, toLocalRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, toLocalScale));
      return _obj;
    }

    public ITweenObj FromLocal(Transform from) {
      _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from.localPosition, _target.localPosition));
      _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from.localRotation, _target.localRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, _target.localScale));
      return _obj;
    }

    public ITweenObj FromLocal(Vector3 fromLocalPosition, Quaternion fromLocalRotation, Vector3 fromLocalScale) {
      _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, fromLocalPosition, _target.localPosition));
      _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, fromLocalRotation, _target.localRotation));
      _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, fromLocalScale, _target.localScale));
      return _obj;
    }

    //LOCAL POSITION
    public ITweenObj LocalPosition(Transform from, Transform to) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from.localPosition, to.localPosition));
    }

    public ITweenObj LocalPosition(Vector3 from, Vector3 to) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from, to));
    }

    public ITweenObj ToLocalPosition(Transform to) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, _target.localPosition, to.localPosition));
    }

    public ITweenObj ToLocalPosition(Vector3 to) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, _target.localPosition, to));
    }

    public ITweenObj FromLocalPosition(Transform from) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from.localPosition, _target.localPosition));
    }

    public ITweenObj FromLocalPosition(Vector3 from) {
      return _obj.AddInterpolator(new TransformLocalPositionInterpolator(_target, from, _target.localPosition));
    }

    //LOCAL ROTATION
    public ITweenObj LocalRotation(Transform from, Transform to) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from.localRotation, to.localRotation));
    }

    public ITweenObj LocalRotation(Quaternion from, Quaternion to) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from, to));
    }

    public ITweenObj ToLocalRotation(Transform to) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, _target.localRotation, to.localRotation));
    }

    public ITweenObj ToLocalRotation(Quaternion to) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, _target.localRotation, to));
    }

    public ITweenObj FromLocalRotation(Transform from) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from.localRotation, _target.localRotation));
    }

    public ITweenObj FromLocalRotation(Quaternion from) {
      return _obj.AddInterpolator(new TransformLocalRotationInterpolator(_target, from, _target.localRotation));
    }

    //LOCAL SCALE
    public ITweenObj LocalScale(Transform from, Transform to) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, to.localScale));
    }

    public ITweenObj LocalScale(Vector3 from, Vector3 to) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from, to));
    }

    public ITweenObj ToLocalScale(Transform to) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, to.localScale));
    }

    public ITweenObj ToLocalScale(Vector3 to) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, _target.localScale, to));
    }

    public ITweenObj FromLocalScale(Transform from) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from.localScale, _target.localScale));
    }

    public ITweenObj FromLocalScale(Vector3 from) {
      return _obj.AddInterpolator(new TransformLocalScaleInterpolator(_target, from, _target.localScale));
    }
  }

}