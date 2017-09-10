using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(AdvancedPropertyAttribute), true)]
public class AdvancedPropertyDrawer : PropertyDrawer {

  private IEnumerable<AdvancedPropertyAttribute> attributes {
    get {
      foreach (object o in fieldInfo.GetCustomAttributes(typeof(AdvancedPropertyAttribute), true)) {
        yield return o as AdvancedPropertyAttribute;
      }
    }
  }

  private bool useSlider {
    get {
      return fieldInfo.GetCustomAttributes(typeof(RangeAttribute), true).Length != 0;
    }
  }

  private float sliderLeft {
    get {
      return (fieldInfo.GetCustomAttributes(typeof(RangeAttribute), true)[0] as RangeAttribute).min;
    }
  }

  private float sliderRight {
    get {
      return (fieldInfo.GetCustomAttributes(typeof(RangeAttribute), true)[0] as RangeAttribute).max;
    }
  }

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    float defaultLabelWidth = EditorGUIUtility.labelWidth;
    float fieldWidth = position.width - EditorGUIUtility.labelWidth;

    bool canUseDefaultDrawer = true;
    bool shouldDisable = false;

    AdvancedPropertyAttribute fieldOverride = null;
    foreach (var a in attributes) {
      EditorGUIUtility.labelWidth -= a.getBeforeLabelWidth();
      EditorGUIUtility.labelWidth -= a.getAfterLabelWidth();
      fieldWidth -= a.getBeforeFieldWidth();
      fieldWidth -= a.getAfterFieldWidth();

      if (a.getAfterLabelWidth() != 0 || a.getBeforeFieldWidth() != 0) {
        canUseDefaultDrawer = false;
      }

      shouldDisable |= a.shouldDisable(property);

      if (a.doesOverrideField()) {
        if (fieldOverride != null) {
          Debug.LogError("Cannot have 2 advanced attributes that both override the field drawing");
          return;
        }
        fieldOverride = a;
      }
    }

    if (fieldOverride != null && !canUseDefaultDrawer) {
      Debug.LogError("Cannot have an advanced attribute drawer that draws a custom field, and also have an adavanced attribute drawer that draws between label and field!");
      return;
    }

    Rect r = position;
    EditorGUI.BeginDisabledGroup(shouldDisable);

    foreach (var a in attributes) {
      float w = a.getBeforeLabelWidth();
      if (w != 0.0f) {
        r.width = w;
        r.height = a.getMinHeight();
        a.doBeforeLabelGUI(r, property);
        r.x += r.width;
      }
    }

    if (canUseDefaultDrawer) {
      r.width = EditorGUIUtility.labelWidth + fieldWidth;

      if (fieldOverride != null) {
        r.height = fieldOverride.getMinHeight();
        fieldOverride.doCompleteField(r, property, label);
      } else {
        r.height = EditorGUIUtility.singleLineHeight;

        if (useSlider) {
          if (property.propertyType == SerializedPropertyType.Integer) {
            property.intValue = EditorGUI.IntSlider(r, label, property.intValue, (int)sliderLeft, (int)sliderRight);
          } else if (property.propertyType == SerializedPropertyType.Float) {
            property.floatValue = EditorGUI.Slider(r, label, property.floatValue, sliderLeft, sliderRight);
          } else {
            EditorGUI.PropertyField(r, property, label);
          }
        } else {
          EditorGUI.PropertyField(r, property, label);
        }
      }

      r.x += r.width;
    } else {
      r.width = EditorGUIUtility.labelWidth;
      r.height = EditorGUIUtility.singleLineHeight;
      r = EditorGUI.PrefixLabel(r, label);

      foreach (var a in attributes) {
        float w = a.getAfterLabelWidth();
        if (w != 0.0f) {
          r.width = w;
          r.height = a.getMinHeight();
          a.doAfterLabelGUI(r, property);
          r.x += r.width;
        }
      }

      foreach (var a in attributes) {
        float w = a.getBeforeFieldWidth();
        if (w != 0.0f) {
          r.width = w;
          r.height = a.getMinHeight();
          a.doBeforeFieldGUI(r, property);
          r.x += r.width;
        }
      }

      r.width = fieldWidth;
      r.height = EditorGUIUtility.singleLineHeight;
      EditorGUI.PropertyField(r, property, GUIContent.none);
      r.x += r.width;
    }

    foreach (var a in attributes) {
      float w = a.getAfterFieldWidth();
      if (w != 0.0f) {
        r.width = w;
        r.height = a.getMinHeight();
        a.doAfterFieldGUI(r, property);
        r.x += r.width;
      }
    }

    EditorGUI.EndDisabledGroup();

    foreach (var a in attributes) {
      a.constrainValue(property);
    }

    EditorGUIUtility.labelWidth = defaultLabelWidth;
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    float maxHeight = base.GetPropertyHeight(property, label);
    foreach (var a in attributes) {
      maxHeight = Mathf.Max(a.getMinHeight(), maxHeight);
    }
    return maxHeight;
  }
}
