using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SearchableList : DynamicList<string> {
    public InputField searchField;
    public List<string> elements = new List<string>();

    private string _searchString = null;

    void Awake() {
        generateList(elements);
    }

    void Update() {
        if (_searchString != searchField.text) {
            _searchString = searchField.text;

            List<string> filteredList = new List<string>();

            foreach (string element in elements) {
                if (element.ToLower().Contains(_searchString.ToLower())) {
                    filteredList.Add(element);
                }
            }

            generateList(filteredList);
        }
    }
}
