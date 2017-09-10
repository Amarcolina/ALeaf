using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class MaterialInterpolatorSelector {
    private Material _target;
    private TweenObj _obj;

    public MaterialInterpolatorSelector(TweenObj obj, Material target) {
      _target = target;
      _obj = obj;
    }

    public ITweenObj Color(Color from, Color to, string propertyName = "_Color") {
      return _obj.AddInterpolator(new MaterialColorInterpolator(_target, propertyName, from, to));
    }

    public ITweenObj ToColor(Color to, string propertyName = "_Color") {
      return _obj.AddInterpolator(new MaterialColorInterpolator(_target, propertyName, _target.GetColor(propertyName), to));
    }

    public ITweenObj FromColor(Color from, string propertyName = "_Color") {
      return _obj.AddInterpolator(new MaterialColorInterpolator(_target, propertyName, from, _target.GetColor(propertyName)));
    }

    public ITweenObj Alpha(float from, float to, string propertyName = "_Color") {
      return _obj.AddInterpolator(new MaterialAlphaInterpolator(_target, propertyName, from, to));
    }

    public ITweenObj ToAlpha(float to, string propertyName = "_Color") {
      return _obj.AddInterpolator((new MaterialAlphaInterpolator(_target, propertyName, _target.GetColor(propertyName).a, to)));
    }

    public ITweenObj FromAlpha(float from, string propertyName = "_Color") {
      return _obj.AddInterpolator(new MaterialAlphaInterpolator(_target, propertyName, from, _target.GetColor(propertyName).a));
    }

    public ITweenObj Value(float from, float to, string propertyName) {
      return _obj.AddInterpolator(new MaterialValueInterpolator(_target, propertyName, from, to));
    }

    public ITweenObj ToValue(float to, string propertyName) {
      return _obj.AddInterpolator(new MaterialValueInterpolator(_target, propertyName, _target.GetFloat(propertyName), to));
    }

    public ITweenObj FromValue(float from, string propertyName) {
      return _obj.AddInterpolator(new MaterialValueInterpolator(_target, propertyName, from, _target.GetFloat(propertyName)));
    }
  }

}