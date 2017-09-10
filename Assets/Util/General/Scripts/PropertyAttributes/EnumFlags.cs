using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class EnumFlags : AdvancedPropertyAttribute {
    public EnumFlags() { }

#if UNITY_EDITOR
    public override bool doesOverrideField() {
        return true;
    }

    public override void doCompleteField(Rect rect, SerializedProperty property, GUIContent label) {
        property.intValue = EditorGUI.MaskField(rect, label, property.intValue, property.enumNames);
    }
#endif
}