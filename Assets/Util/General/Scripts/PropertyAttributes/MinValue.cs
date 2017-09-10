using UnityEngine;
using System.Collections;

public class MinValue : AdvancedPropertyAttribute {
    public float minValue;

    public MinValue(float minValue) {
        this.minValue = minValue;
    }

#if UNITY_EDITOR
    public override void constrainValue(UnityEditor.SerializedProperty property) {
        if (property.propertyType == UnityEditor.SerializedPropertyType.Float) {
            property.floatValue = Mathf.Max(minValue, property.floatValue);
        } else if (property.propertyType == UnityEditor.SerializedPropertyType.Integer) {
            property.intValue = Mathf.Max((int)minValue, property.intValue);
        } else {
            Debug.LogWarning("Should not use MinValue for fields that are not float or int!");
        }
    }
#endif
}