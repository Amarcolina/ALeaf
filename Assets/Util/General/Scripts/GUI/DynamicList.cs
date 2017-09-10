using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IInitializeable<T> {
    void initializeWithData(T t);
}

public abstract class DynamicList<T> : MonoBehaviour {
    public GameObject listElement;

    private RectTransform _rectTransform;
    private List<GameObject> _listElements = new List<GameObject>();

    void OnValidate() {
        if (listElement.GetComponent<RectTransform>() == null) {
            listElement = null;
            return;
        }
        if (listElement.GetComponent(typeof(IInitializeable<T>)) == null) {
            listElement = null;
            return;
        }
    }

    public void generateList(List<T> elementData) {
        float inset = 0;

        _rectTransform = GetComponent<RectTransform>();

        foreach (GameObject obj in _listElements) {
            DestroyImmediate(obj);
        }

        foreach (T data in elementData) {
            GameObject elementInstance = Instantiate<GameObject>(listElement);
            _listElements.Add(elementInstance);

            RectTransform rectTransform = elementInstance.GetComponent<RectTransform>();
            rectTransform.SetParent(_rectTransform, false);


            IInitializeable<T> initializable = elementInstance.GetComponent(typeof(IInitializeable<T>)) as IInitializeable<T>;
            initializable.initializeWithData(data);

            rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, inset, rectTransform.sizeDelta.y);
            inset += rectTransform.rect.height;
        }

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, inset);
    }
}
