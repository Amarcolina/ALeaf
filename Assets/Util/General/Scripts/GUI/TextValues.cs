using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TextValues : MonoBehaviour {
  private Text _text;
  private string _originalText;

  private static bool _shouldUpdate = false;
  private static Dictionary<string, string> _keyToValue = new Dictionary<string, string>();

  void Awake() {
    _text = GetComponent<Text>();
    _originalText = _text.text;

    if (_originalText == null) {
      _originalText = "";
    }

    if (_text == null) {
      Debug.LogWarning("UXInfo without Text");
      Destroy(this);
    }
  }

  void OnEnable() {
    updateText();
  }

  void Update() {
    if (_shouldUpdate) {
      updateText();
    }
  }

  void LateUpdate() {
    _shouldUpdate = false;
  }

  private void updateText() {
    string text = _originalText;
    foreach (var pair in _keyToValue) {
      text = text.Replace("<" + pair.Key + ">", pair.Value);
    }
    _text.text = text;
  }

  public static void setValue(string key, object value) {
    _keyToValue[key] = value.ToString();
    _shouldUpdate = true;
  }
}