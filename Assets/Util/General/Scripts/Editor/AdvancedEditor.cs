using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class AdvancedEditor : Editor {
  protected Dictionary<string, Action<SerializedProperty>> _specifiedDrawers = new Dictionary<string, Action<SerializedProperty>>();

  /**
   * Specify a callback to be used to draw a specific named property.  Should be called in OnEnable
   */
  protected void specifyCustomDrawer(string propertyName, Action<SerializedProperty> propertyDrawer) {
    if (serializedObject.FindProperty(propertyName) != null) {
      _specifiedDrawers[propertyName] = propertyDrawer;
    }else{
      Debug.LogWarning("Specified a custom drawer for the nonexistant property [" + propertyName + "] !\nWas it renamed or deleted?");
    }
  }

  /* 
   * This method draws all visible properties, mirroring the default behavior of OnInspectorGUI. 
   * Individual properties can be specified to have custom drawers.
   */
  public override void OnInspectorGUI() {
    SerializedProperty iterator = serializedObject.GetIterator();
    bool isFirst = true;

    while (iterator.NextVisible(isFirst)) {
      Action<SerializedProperty> customDrawer;

      if (_specifiedDrawers.TryGetValue(iterator.name, out customDrawer)) {
        customDrawer(iterator);
      } else {
        EditorGUILayout.PropertyField(iterator, true);
      }

      isFirst = false;
    }

    serializedObject.ApplyModifiedProperties();
  }
}
