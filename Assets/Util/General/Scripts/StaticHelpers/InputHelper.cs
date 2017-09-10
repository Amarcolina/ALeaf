using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class InputHelper {
  private static Dictionary<KeyCode, float> _canRepressTime = new Dictionary<KeyCode, float>();

  public static bool getKeyDownRepeat(KeyCode key, float delay = 0.5f, float pressRepeat = 0.05f) {
    if (Input.GetKeyDown(key)) {
      _canRepressTime[key] = Time.time + delay;
      return true;
    }

    float canRepressTime;
    if (!_canRepressTime.TryGetValue(key, out canRepressTime)) {
      _canRepressTime[key] = 0;
      canRepressTime = 0;
    }

    //if we are not at the repress time yet, always return false;
    if (Time.time < _canRepressTime[key]) {
      return false;
    }

    if (Input.GetKey(key)) {
      _canRepressTime[key] = Time.time + pressRepeat;
      return true;
    }

    return false;
  }
}
