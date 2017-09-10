using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TweenInternal;

public static class Tween {
  public static ITweenObj Value(float from, float to, Action<float> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Color from, Color to, Action<Color> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Color32 from, Color32 to, Action<Color32> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Vector2 from, Vector2 to, Action<Vector2> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Vector3 from, Vector3 to, Action<Vector3> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Vector4 from, Vector4 to, Action<Vector4> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static ITweenObj Value(Quaternion from, Quaternion to, Action<Quaternion> onValue) {
    return new TweenObj().Value(from, to, onValue);
  }

  public static MaterialInterpolatorSelector Target(Material material) {
    return new TweenObj().Target(material);
  }

  public static TransformInterpolatorSelector Target(Transform transform) {
    return new TweenObj().Target(transform);
  }

  public static CanvasGroupInterpolatorSelector Target(CanvasGroup canvasGroup) {
    return new TweenObj().Target(canvasGroup);
  }

  public static void AfterDelay(float delay, Action action) {
    new TweenObj().OverTime(delay).OnComplete(action);
  }
}
