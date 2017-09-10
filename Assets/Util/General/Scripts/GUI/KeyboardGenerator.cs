using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyboardGenerator : MonoBehaviour {
    public static string[] ROWS = { "zxcvbnm", "asdfghjkl", "qwertyuiop" };

    public GameObject keyPrefab;
    public GameObject spaceBarPrefab;
    public GameObject backButtonPrefab;
    public GameObject capsLockPrefab;

    public Vector2 keySpacing = Vector2.zero;
    public Vector2 spaceBarSize = Vector2.one;
    public InputField inputField;
    public bool startWithCaps = true;
    public bool autoCapAfterSpace = true;

    private RectTransform _rectTransform;
    private List<KeyboardKey> _keys = new List<KeyboardKey>();
    private Toggle capsLockToggle = null;

    private float keyHeight;
    private float keyWidth;
    private float prefabScale;

    void OnValidate() {
        validatePrefab(ref keyPrefab);
        validatePrefab(ref spaceBarPrefab);
        validatePrefab(ref backButtonPrefab);
        validatePrefab(ref capsLockPrefab);
    }

    private void validatePrefab(ref GameObject obj) {
        if (obj == null) {
            return;
        }
        if (obj.GetComponent<KeyboardKey>() == null) {
            obj = null;
            return;
        }
        if (obj.GetComponent<RectTransform>() == null) {
            obj = null;
            return;
        }
    }

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        regenerate();
        setCaps(startWithCaps);
    }

    public void setCaps(bool useCaps) {
        capsLockToggle.isOn = useCaps;
        updateCaps();
    }

    private void updateCaps() {
        foreach (KeyboardKey key in _keys) {
            if (capsLockToggle.isOn) {
                key.character = key.character.ToUpper();
            } else {
                key.character = key.character.ToLower();
            }
        }
    }

    public void regenerate() {
        foreach (KeyboardKey key in _keys) {
            DestroyImmediate(key.gameObject);
        }

        RectTransform rectTransform = GetComponent<RectTransform>();

        string topRow = ROWS[2];
        keyWidth = (rectTransform.sizeDelta.x - (topRow.Length - 1) * keySpacing.x) / topRow.Length;
        prefabScale = (keyWidth / keyPrefab.GetComponent<RectTransform>().sizeDelta.x);
        keyHeight = keyPrefab.GetComponent<RectTransform>().sizeDelta.y * prefabScale;

        RectTransform spaceBar = instantiateKey(spaceBarPrefab);
        spaceBar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, keyHeight * spaceBarSize.y);
        spaceBar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _rectTransform.sizeDelta.x * 0.5f * (1 - spaceBarSize.x), _rectTransform.sizeDelta.x * spaceBarSize.x);
        spaceBar.GetComponent<KeyboardKey>().init(
            "",
            () => {
                inputField.text += " ";
                if (autoCapAfterSpace) {
                    setCaps(true);
                }
            });

        RectTransform capsLock = instantiateKey(capsLockPrefab, RectTransform.Edge.Left, keyHeight / 2.0f, 0);
        capsLockToggle = capsLock.GetComponent<Toggle>();
        capsLock.GetComponent<KeyboardKey>().init(
            "",
            () => {
                updateCaps();
            });

        RectTransform backspace = instantiateKey(backButtonPrefab, RectTransform.Edge.Right, keyHeight / 2.0f, 0.0f);
        backspace.GetComponent<KeyboardKey>().init(
            "",
            () => {
                if (inputField.text != "") {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
                }
            });

        float verticalInset = keyHeight + keySpacing.y;

        foreach (string row in ROWS) {
            float horizontalKeySpace = (row.Length - 1) * keySpacing.x + row.Length * keyWidth;
            float horizontalInset = (rectTransform.sizeDelta.x - horizontalKeySpace) / 2.0f;

            foreach (char key in row) {
                RectTransform keyRect = instantiateKey(keyPrefab, RectTransform.Edge.Left, verticalInset, horizontalInset);
                KeyboardKey keyComponent = keyRect.GetComponent<KeyboardKey>();

                string keyString = key.ToString();
                keyComponent.init(
                    keyString,
                    () => {
                        inputField.text += keyComponent.character;
                        if (capsLockToggle.isOn) {
                            setCaps(false);
                        }
                    });

                horizontalInset += keyWidth + keySpacing.x;
            }

            verticalInset += keyHeight + keySpacing.y;
        }

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, verticalInset);
    }

    private RectTransform instantiateKey(GameObject prefab) {
        GameObject obj = Instantiate<GameObject>(prefab);
        _keys.Add(obj.GetComponent<KeyboardKey>());

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.SetParent(_rectTransform, false);
        rt.SetAsFirstSibling();

        return rt;
    }

    private RectTransform instantiateKey(GameObject prefab, RectTransform.Edge side, float verticalInset, float horizontalInset) {
        RectTransform rt = instantiateKey(prefab);

        rt.localScale *= prefabScale;
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, verticalInset, rt.sizeDelta.y * prefabScale);
        rt.SetInsetAndSizeFromParentEdge(side, horizontalInset, rt.sizeDelta.x * prefabScale);
        rt.sizeDelta = new Vector2(keyWidth, keyHeight) / prefabScale;

        return rt;
    }
}
