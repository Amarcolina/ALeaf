using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

public class DisableIf : AdvancedPropertyAttribute {
    public readonly string propertyName;
    public readonly bool equalTo;

    public DisableIf(string propertyName, bool equalTo = true) {
        this.propertyName = propertyName;
        this.equalTo = equalTo;
    }

#if UNITY_EDITOR
    public override bool shouldDisable(SerializedProperty property) {
        SerializedProperty prop = property.serializedObject.FindProperty(propertyName);
        return prop.boolValue == equalTo;
    }
#endif
}
