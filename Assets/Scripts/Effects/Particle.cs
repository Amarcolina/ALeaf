using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Particle : MonoBehaviour {

  [SerializeField]
  private float _maxOffset = 0;

  [SerializeField]
  private Transform _halo;

  private float _noiseOffset = 0;

  void Start() {
    Tween.Target(GetComponent<Renderer>().material).Value(0, 0.21f, "_EmissionGain").Play();
  }

  void Update() {
    /*
    _noiseOffset += 0.01f;

    float dx = (Mathf.PerlinNoise(_noiseOffset, _noiseOffset * 0.23f + 0.34f) - 0.5f) * _maxOffset;
    float dy = (Mathf.PerlinNoise(_noiseOffset + 5, _noiseOffset * 0.23f + 6.34f) - 0.5f) * _maxOffset;

    _halo.transform.localPosition = new Vector3(dx, dy, 0);
     * */
  }


}
