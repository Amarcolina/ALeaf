using UnityEngine;
using System.Collections;

/* This behaviour will cycle through enabling behaviours one by one, whenever the cycle key
 * combo is pressed.  This script will select the first enabled behaviour in the list to be
 * the first of the cycle, or the first behaviour if there are no enabled behaviours.
 * 
 * Each time the key combo is pressed, the previously enabled behaviour is disabled and
 * the next behaviour in the list is enabled.
 */
public class CycleBehaviours : MonoBehaviour {

  [SerializeField]
  private KeyCode _cycleUnlockKey = KeyCode.LeftShift;

  [SerializeField]
  private KeyCode _cycleKey = KeyCode.R;

  [SerializeField]
  private Behaviour[] _behaviours;

  private int _enabledIndex;

  void Start() {
    _enabledIndex = -1;

    for (int i = 0; i < _behaviours.Length; i++) {
      if (_enabledIndex == -1) {
        if (_behaviours[i].enabled) {
          _enabledIndex = i;
        }
      } else {
        _behaviours[i].enabled = false;
      }
    }

    if (_enabledIndex == -1) {
      _enabledIndex = 0;
      _behaviours[_enabledIndex].enabled = true;
    }
  }

  void Update() {
    if ((_cycleUnlockKey == KeyCode.None || Input.GetKey(_cycleUnlockKey)) && Input.GetKeyDown(_cycleKey)) {
      _behaviours[_enabledIndex].enabled = false;
      _enabledIndex = (_enabledIndex + 1) % _behaviours.Length;
      _behaviours[_enabledIndex].enabled = true;
    }
  }
}
