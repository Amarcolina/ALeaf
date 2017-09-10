using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TweenInternal {

  public class TweenRunner : MonoBehaviour {
    private TweenObj[] _runningTweens = new TweenObj[16];
    private int _runningCount = 0;

    private static TweenRunner _instance = null;
    public static TweenRunner Instance {
      get {
        if (_instance == null) {
          _instance = FindObjectOfType<TweenRunner>();
          if (_instance == null) {
            _instance = new GameObject("Tween Runner").AddComponent<TweenRunner>();
            _instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
          }
        }
        return _instance;
      }
    }

    void Update() {
      for (int i = _runningCount - 1; i >= 0; --i) {
        try {
          if (_runningTweens[i].StepProgress()) {
            RemoveTween(i);
          }
        } catch (Exception e) {
          RemoveTween(i);
          Debug.LogError("Error occured inside of tween!  Tween has been terminated");
          Debug.LogException(e);
        }
      }
    }

    public void AddTween(TweenObj obj) {
      if (_runningTweens.Length <= _runningCount) {
        TweenObj[] newArray = new TweenObj[_runningTweens.Length * 2];
        _runningTweens.CopyTo(newArray, 0);
        _runningTweens = newArray;
      }

      obj.SetRunnerIndex(_runningCount);
      _runningTweens[_runningCount++] = obj;
    }

    public void RemoveTween(int index) {
      --_runningCount;
      _runningTweens[_runningCount].SetRunnerIndex(_runningCount);
      _runningTweens[index].SetRunnerIndex(-1);
      _runningTweens[index] = _runningTweens[_runningCount];
    }
  }

}