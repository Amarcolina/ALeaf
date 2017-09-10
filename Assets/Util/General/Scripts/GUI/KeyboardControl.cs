using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class KeyboardControl : MonoBehaviour, ISelectHandler {
    public RectTransform keybaordRect;
    public RectTransform listRect;

    
    [MinValue(0)]
    public float transitionTime = 0.4f;

    public bool keyboardOut = false;

    protected InputField _input;
    private float inY;
    private float _keyboardOutPercent = 0.0f;
    private float _listMaxHeight;

    void Awake() {
        _input = GetComponent<InputField>();
    }

    void Start() {
        inY = keybaordRect.sizeDelta.y;
        _listMaxHeight = listRect.sizeDelta.y;
    }

    void Update() {
        _keyboardOutPercent = Mathf.MoveTowards(_keyboardOutPercent, keyboardOut ? 1.0f : 0.0f, Time.deltaTime / transitionTime);
        float smoothPercent = Mathf.Pow(1.0f - _keyboardOutPercent, 2.0f);

        float y = Mathf.Lerp(0, inY, smoothPercent);
        keybaordRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, -y, keybaordRect.sizeDelta.y);
        y = Mathf.Lerp(inY, 0, smoothPercent);
        listRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _listMaxHeight - y);
    }

    public void showKeyboard() {
        keyboardOut = true;
    }

    public void hideKeyboard() {
        keyboardOut = false;
    }

    public void OnSelect(BaseEventData data) {
        showKeyboard();
    }
}
