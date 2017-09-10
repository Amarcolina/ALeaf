using UnityEngine;
using System.Collections;

public class WorldToScreenUtil {
  public static Vector3 WorldToScreen(Camera camera, Vector3 worldPosition) {
    if (camera.transform.InverseTransformPoint(worldPosition).z > 0.0f) {
      return camera.WorldToScreenPoint(worldPosition);
    } else {
      return Vector3.zero;
    }
  }

  private static void GetMaxRect(Camera camera, Vector3 worldPosition, ref Rect rect) {
    Vector3 cameraPosition = WorldToScreen(camera, worldPosition);
    if (cameraPosition.z > 0.0f) {
      rect.xMin = Mathf.Min(rect.xMin, cameraPosition.x);
      rect.xMax = Mathf.Max(rect.xMax, cameraPosition.x);
      rect.yMin = Mathf.Min(rect.yMin, cameraPosition.y);
      rect.yMax = Mathf.Max(rect.yMax, cameraPosition.y);
    }
  }

  public static Rect ProcessBounds(Camera camera, Bounds bounds) {
    Rect rect = new Rect(camera.WorldToScreenPoint(bounds.center), Vector2.zero);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(1, 1, 1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(1, 1, -1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(1, -1, 1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(1, -1, -1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1, 1, 1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1, 1, -1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1, -1, 1)), ref rect);
    GetMaxRect(camera, bounds.center + Vector3.Scale(bounds.extents, new Vector3(-1, -1, -1)), ref rect);
    return rect;
  }

  public static Rect ProcessMesh(Camera camera, Vector3[] vertices, Renderer renderer) {
    if (vertices.Length == 0)
      return new Rect();
    Rect rect = new Rect(camera.WorldToScreenPoint(renderer.transform.position), Vector2.zero);
    for (int i = 0; i < vertices.Length; ++i) {
      GetMaxRect(camera, renderer.transform.TransformPoint(vertices[i]), ref rect);
    }
    return rect;
  }
}
