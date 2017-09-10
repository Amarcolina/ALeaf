using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeyboardKey : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler {
    public List<Text> relevantTextElements = new List<Text>();
    public GameObject pressedHighlight = null;

    private string _character;
    private Action _action;
    private InputField _inputField;

    public string character {
        get {
            return _character;
        }
        set {
            _character = value;
            foreach (Text t in relevantTextElements) {
                t.text = _character;
            }
        }
    }

    public void init(string character, Action action) {
        this.character = character;

        _character = character;
        _action = action;
    }

    public void OnPointerDown(PointerEventData data) {
        if (pressedHighlight != null) {
            pressedHighlight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData data) {
        if (pressedHighlight != null) {
            pressedHighlight.SetActive(false);
        }
    }

    public void OnPointerUp(PointerEventData data) {
        if (pressedHighlight != null) {
            pressedHighlight.SetActive(false);
        }
    }

    public void OnKeyActivated() {
        if (_action != null) {
            _action();
        }
    }
}
