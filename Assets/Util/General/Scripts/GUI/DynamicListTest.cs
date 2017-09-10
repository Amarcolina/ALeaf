using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicListTest : DynamicList<string> {
    public List<string> values = new List<string>();

    void Awake() {
        generateList(values);
    }
}
