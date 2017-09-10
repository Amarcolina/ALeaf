using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class Incrementable : AdvancedPropertyAttribute {
    public const float BUTTON_WIDTH = 20;

#if UNITY_EDITOR
    public override float getAfterFieldWidth() {
        return BUTTON_WIDTH * 2;
    }

    public override void doAfterFieldGUI(Rect rect, SerializedProperty property) {
        if (property.propertyType != UnityEditor.SerializedPropertyType.Integer) {
            Debug.LogWarning("Cannot use Incrementable for a field type other than Int!");
            return;
        }

        rect.width = BUTTON_WIDTH;

        if (GUI.Button(rect, "-")) {
            property.intValue--;
        }

        rect.x += rect.width;

        if (GUI.Button(rect, "+")) {
            property.intValue++;
        }
    }
#endif
}