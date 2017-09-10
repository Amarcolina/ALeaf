using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class GizmoUtil {
    private static Stack<Matrix4x4> _matrixStack = new Stack<Matrix4x4>();

    public static void pushMatrix() {
        _matrixStack.Push(Gizmos.matrix);
    }

    public static void popMatrix() {
        Gizmos.matrix = _matrixStack.Pop();
    }

    public static void relativeTo(GameObject obj) {
        Gizmos.matrix = obj.transform.localToWorldMatrix;
    }
}
