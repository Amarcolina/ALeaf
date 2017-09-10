using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class TweenObj : ITweenObj {
    private static float linearTween(float value) {
      return value;
    }

    private static float smoothTween(float value) {
      return Mathf.SmoothStep(0.0f, 1.0f, value);
    }

    private static float smoothInTween(float value) {
      return 1.0f - Mathf.Pow(1.0f - value, 2.0f);
    }

    private static float smoothOutTween(float value) {
      return Mathf.Pow(value, 2.0f);
    }

    #region SETTING VARIABLES
    private Interpolator[] _interpolators = new Interpolator[2];
    private int _interpolatorCount = 0;

    private Func<float, float> _smoothFunc = linearTween;
    private Action<float> _onProgressAction = null;
    private Action _onCompleteAction = null;
    #endregion

    #region RUNTIME VARIABLES
    private float _percent = 0.0f;
    private float _delta = 1.0f;
    private TweenDirection _direction = TweenDirection.FORWARD;
    private float _goalPercent = 1.0f;

    private int _runnerIndex = -1;
    #endregion

    #region INTERFACE METHODS
    public ITweenObj Value(float from, float to, Action<float> onValue) {
      return AddInterpolator(new FloatInterpolator(from, to, onValue));
    }

    public ITweenObj Value(Color from, Color to, Action<Color> onValue) {
      return AddInterpolator(new ColorInterpolator(from, to, onValue));
    }

    public ITweenObj Value(Color32 from, Color32 to, Action<Color32> onValue) {
      return AddInterpolator(new Color32Interpolator(from, to, onValue));
    }

    public ITweenObj Value(Vector2 from, Vector2 to, Action<Vector2> onValue) {
      return AddInterpolator(new Vector2Interpolator(from, to, onValue));
    }

    public ITweenObj Value(Vector3 from, Vector3 to, Action<Vector3> onValue) {
      return AddInterpolator(new Vector3Interpolator(from, to, onValue));
    }

    public ITweenObj Value(Vector4 from, Vector4 to, Action<Vector4> onValue) {
      return AddInterpolator(new Vector4Interpolator(from, to, onValue));
    }

    public ITweenObj Value(Quaternion from, Quaternion to, Action<Quaternion> onValue) {
      return AddInterpolator(new QuaternionInterpolator(from, to, onValue));
    }

    public MaterialInterpolatorSelector Target(Material material) {
      return new MaterialInterpolatorSelector(this, material);
    }

    public TransformInterpolatorSelector Target(Transform transform) {
      return new TransformInterpolatorSelector(this, transform);
    }

    public CanvasGroupInterpolatorSelector Target(CanvasGroup canvasGroup) {
      return new CanvasGroupInterpolatorSelector(this, canvasGroup);
    }

    public bool IsRunning {
      get {
        return _runnerIndex != -1;
      }
      set {
        if (IsRunning != value) {
          if (IsRunning) {
            Pause();
          } else {
            Play();
          }
        }
      }
    }

    public TweenDirection Direction {
      get {
        return _direction;
      }
      set {
        _direction = value;
        _delta = Mathf.Abs(_delta) * (int)_direction;
        _goalPercent = _direction == TweenDirection.FORWARD ? 1.0f : 0.0f;
      }
    }

    public float TimeLeft {
      get {
        return Mathf.Abs((_percent - _goalPercent) / _delta);
      }
    }

    public float Progress {
      get {
        return _percent;
      }
      set {
        _percent = value;
        if (!IsRunning) {
          float progress = _smoothFunc(_percent);
          interpolatePercent(progress);
        }
      }
    }

    public ITweenObj OverTime(float forTime) {
      _delta = (int)_direction / forTime;
      return this;
    }

    public ITweenObj AtRate(float rate) {
      return OverTime(_interpolators[0].GetLength() / rate);
    }

    public ITweenObj Smooth(TweenType TweenType) {
      switch (TweenType) {
        case TweenType.LINEAR:
          _smoothFunc = linearTween;
          break;
        case TweenType.SMOOTH:
          _smoothFunc = smoothTween;
          break;
        case TweenType.SMOOTH_END:
          _smoothFunc = smoothInTween;
          break;
        case TweenType.SMOOTH_START:
          _smoothFunc = smoothOutTween;
          break;
        default:
          throw new Exception("Unecpected Tween type " + TweenType);
      }
      return this;
    }

    public ITweenObj Smooth(Func<float, float> TweenFunc) {
      _smoothFunc = TweenFunc;
      return this;
    }

    public ITweenObj Smooth(AnimationCurve TweenCurve) {
      _smoothFunc = TweenCurve.Evaluate;
      return this;
    }

    public ITweenObj OnProgress(Action<float> onProgress) {
      _onProgressAction = onProgress;
      return this;
    }

    public ITweenObj OnComplete(Action onComplete) {
      _onCompleteAction = onComplete;
      return this;
    }

    /* Starts this tween.  If it was paused, it will resume from the same position and
     * direction from where it left off.
     */
    public WaitForSeconds Play() {
      //If we are already running, or if we are already at our destination, no need
      //to restart the tween.
      if (IsRunning || _percent == _goalPercent) {
        return new WaitForSeconds(TimeLeft);
      }

      TweenRunner.Instance.AddTween(this);

      return new WaitForSeconds(TimeLeft);
    }

    public WaitForSeconds Play(TweenDirection direction) {
      this.Direction = direction;
      return Play();
    }

    /* Stops the tween.  When started again it will resume from it's current position.
     */
    public void Pause() {
      if (_runnerIndex != -1) {
        TweenRunner.Instance.RemoveTween(_runnerIndex);
      }
    }

    /* Stops the coroutine.  When started again it will start from the begining.
     */
    public void Stop() {
      _percent = 0.0f;
      Direction = TweenDirection.FORWARD;
      Pause();
    }
    #endregion

    #region INTERNAL METHODS
    public void SetRunnerIndex(int index) {
      _runnerIndex = index;
    }

    public ITweenObj AddInterpolator(Interpolator interpolator) {
      if (_interpolators.Length <= _interpolatorCount) {
        Interpolator[] newArray = new Interpolator[_interpolators.Length * 2];
        _interpolators.CopyTo(newArray, 0);
        _interpolators = newArray;
      }

      _interpolators[_interpolatorCount++] = interpolator;
      return this;
    }

    public bool StepProgress() {
      _percent += _delta * Time.deltaTime;
      if (_percent * (int)_direction >= _goalPercent) {
        _percent = _goalPercent;
      }

      float progress = _smoothFunc(_percent);

      if (_onProgressAction != null) {
        _onProgressAction(progress);
      }

      interpolatePercent(progress);

      if (_percent == _goalPercent) {
        if (_onCompleteAction != null) {
          _onCompleteAction();
        }

        return true;
      }

      return false;
    }

    private void interpolatePercent(float percent) {
      for (int i = _interpolatorCount - 1; i >= 0; --i) {
        Interpolator interpolator = _interpolators[i];
        if (interpolator.IsValid()) {
          _interpolators[i].Interpolate(percent);
        } else {
          _interpolators[i] = _interpolators[--_interpolatorCount];
        }
      }
    }
    #endregion
  }

}