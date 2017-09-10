using UnityEngine;
using System.Collections;

public class MaxValue : AdvancedPropertyAttribute {
    public float maxValue;

    public MaxValue(float maxValue) {
        this.maxValue = maxValue;
    }

#if UNITY_EDITOR
    public override void constrainValue(UnityEditor.SerializedProperty property) {
        if (property.propertyType == UnityEditor.SerializedPropertyType.Float) {
            property.floatValue = Mathf.Min(maxValue, property.floatValue);
        } else if (property.propertyType == UnityEditor.SerializedPropertyType.Integer) {
            property.intValue = Mathf.Min((int)maxValue, property.intValue);
        } else {
            Debug.LogWarning("Should not use MaxValue for fields that are not float or int!");
        }
    }
#endif
}