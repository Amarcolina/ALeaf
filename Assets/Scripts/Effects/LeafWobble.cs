using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LeafWobble : MonoBehaviour {

  [SerializeField]
  private Transform _toFollow;

  [SerializeField]
  private Transform _toRotate;

  public bool enableWobble = false;

  private Quaternion _rot;
  private Vector3 _smoothedPos;

  private Vector2 _prevPos;
  private float _smoothVel = 0;

  private float noisePos = 0;

  void Update() {
    _smoothedPos += (_toFollow.position - _smoothedPos) / 1.2f;
    transform.position = _smoothedPos;

    if (!enableWobble) {
      _toRotate.rotation = _toFollow.rotation;
      return;
    }

    float vel = Vector2.Distance(transform.position, _prevPos);
    _smoothVel += (vel - _smoothVel) / 5.0f;

    _prevPos = transform.position;

    float deltaAngle = Quaternion.Angle(_rot, _toFollow.rotation);
    _rot = Quaternion.Slerp(_rot, _toFollow.rotation, deltaAngle / 45.0f);

    //_toRotate.rotation = _toFollow.rotation;
    //_toRotate.rotation = _rot;
    noisePos += 0.005f + _smoothVel * 0.03f;
    _toRotate.rotation = _rot * Quaternion.Euler((Mathf.PerlinNoise(noisePos, noisePos + 0.35f) - 0.5f) * (50 + _smoothVel * 2000), 0, 0);
  }
}
