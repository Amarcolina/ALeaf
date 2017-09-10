using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public struct CanvasGroupInterpolatorSelector {
    private CanvasGroup _target;
    private TweenObj _obj;

    public CanvasGroupInterpolatorSelector(TweenObj obj, CanvasGroup target) {
      _target = target;
      _obj = obj;
    }

    public ITweenObj Alpha(float from, float to) {
      return _obj.AddInterpolator(new CanvasGroupAlphaInterpolator(_target, from, to));
    }

    public ITweenObj ToAlpha(float to) {
      return _obj.AddInterpolator(new CanvasGroupAlphaInterpolator(_target, _target.alpha, to));
    }

    public ITweenObj FromAlpha(float from) {
      return _obj.AddInterpolator(new CanvasGroupAlphaInterpolator(_target, from, _target.alpha));
    }
  }

}