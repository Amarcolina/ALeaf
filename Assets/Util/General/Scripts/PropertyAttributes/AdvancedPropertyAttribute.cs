using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class AdvancedPropertyAttribute : PropertyAttribute {

#if UNITY_EDITOR
    public virtual void constrainValue(SerializedProperty property) { }

    public virtual bool shouldDisable(SerializedProperty property) {
        return false;
    }

    public virtual float getMinHeight() {
        return EditorGUIUtility.singleLineHeight;
    }

    public virtual bool doesOverrideField() {
        return false;
    }

    public virtual void doCompleteField(Rect rect, SerializedProperty property, GUIContent label) { }

    public virtual float getBeforeLabelWidth() {
        return 0.0f;
    }

    public virtual void doBeforeLabelGUI(Rect rect, SerializedProperty property) { }

    public virtual float getAfterLabelWidth(){
        return 0.0f;
    }

    public virtual void doAfterLabelGUI(Rect rect, SerializedProperty property) {

    }

    public virtual float getBeforeFieldWidth(){
        return 0.0f;
    }

    public virtual void doBeforeFieldGUI(Rect rect, SerializedProperty property) { }

    public virtual float getAfterFieldWidth(){
        return 0.0f;
    }

    public virtual void doAfterFieldGUI(Rect rect, SerializedProperty property) { }
#endif

}
