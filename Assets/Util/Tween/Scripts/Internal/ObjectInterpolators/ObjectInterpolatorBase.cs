using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public abstract class ObjectInterpolatorBase<A, B, C> : Interpolator {
    protected A _target;
    protected B _from;
    protected C _to;

    public ObjectInterpolatorBase(A a, B b, C c) {
      _target = a;
      _from = b;
    }

    public abstract bool IsValid();
    public abstract void Interpolate(float progress);
    public abstract float GetLength();
  }

}