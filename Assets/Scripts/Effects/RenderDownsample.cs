using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class RenderDownsample : MonoBehaviour {

  [Range(16, 4096)]
  [SerializeField]
  private int startingRes = 1024;

  [Range(16, 4096)]
  [SerializeField]
  private int finalRes = 128;

  [SerializeField]
  private Shader _downsampleShader;

  [SerializeField]
  private Transform _quad;

  private Camera _camera;
  private RenderTexture _startingTex;
  private RenderTexture _finalTex;
  private List<RenderTexture> _intermediate;

  private Material _downsampleMat;

  void OnValidate() {
    startingRes = Mathf.ClosestPowerOfTwo(startingRes);
    finalRes = Mathf.ClosestPowerOfTwo(finalRes);
  }

  void Start() {
    _startingTex = new RenderTexture(startingRes, startingRes, 16, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
    _finalTex = new RenderTexture(finalRes, finalRes, 16, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
    _startingTex.filterMode = FilterMode.Point;
    _finalTex.filterMode = FilterMode.Bilinear;

    _camera = GetComponent<Camera>();
    _camera.enabled = false;
    _camera.targetTexture = _startingTex;

    _downsampleMat = new Material(_downsampleShader);

    _intermediate = new List<RenderTexture>();
    for (int i = startingRes / 2; i >= finalRes * 2; i /= 2) {
      var t = new RenderTexture(i, i, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
      t.filterMode = FilterMode.Point;
      _intermediate.Add(t);
    }

    _quad.GetComponent<Renderer>().material.mainTexture = _finalTex;

    float height = Camera.main.orthographicSize * 2 * (finalRes / (float)Screen.height);
    _quad.transform.localScale = Vector3.one * height;
    _camera.orthographicSize = height / 2.0f;
  }

  void LateUpdate() {
    _camera.Render();

    var source = _startingTex;
    for (int i = 0; i < _intermediate.Count; i++) {
      var temp = _intermediate[i];
      Graphics.Blit(source, temp, _downsampleMat);
      source = temp;
    }
    Graphics.Blit(source, _finalTex, _downsampleMat);
  }

}
