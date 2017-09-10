using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public interface Interpolator {
    void Interpolate(float progress);
    float GetLength();
    bool IsValid();
  }

}