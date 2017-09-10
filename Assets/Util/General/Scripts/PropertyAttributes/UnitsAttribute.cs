using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class UnitsAttribute : AdvancedPropertyAttribute {
  public readonly string unitsName;

  public UnitsAttribute(string unitsName) {
    this.unitsName = unitsName;
  }

#if UNITY_EDITOR

  public override float getAfterFieldWidth() {
    return EditorStyles.label.CalcSize(new GUIContent(unitsName)).x;
  }

  public override void doAfterFieldGUI(Rect rect, SerializedProperty property) {
    GUI.Label(rect, unitsName);
  }


#endif
}
