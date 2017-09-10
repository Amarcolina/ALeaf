using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextInitializer : MonoBehaviour, IInitializeable<string> {
    public Text text;
    public void initializeWithData(string data) {
        text.text = data;
    }
}
