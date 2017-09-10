using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public abstract class ValueInterpolatorBase<T> : Interpolator {
    protected T _from;
    protected T _to;
    protected Action<T> _onValue;

    public ValueInterpolatorBase(T from, T to, Action<T> onValue) {
      _from = from;
      _onValue = onValue;
    }

    public virtual bool IsValid() {
      return true;
    }

    public abstract void Interpolate(float progress);
    public abstract float GetLength();
  }

}