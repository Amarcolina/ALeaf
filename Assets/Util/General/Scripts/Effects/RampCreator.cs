using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RampCreator : MonoBehaviour {
  [Tooltip("This gradient will be converted to a 1D texture for use in a shader.")]
  [SerializeField]
  private Gradient _gradient;

  [Tooltip("When this component is a sibling of a Renderer, it will attempt to assign the texture to the material property of this name.")]
  [SerializeField]
  private string _textureName = "_MainTex";

  [Tooltip("You can set the more advanced settings of the texture itself using these values.  Each value is basically a direct input into the Texture creation.")]
  [SerializeField]
  private TextureSettings _textureSettings = new TextureSettings();

  /* We put this into an internal class so that it shows up as a dropdown in the inspector,
   * hiding the more unused and advanced functions from view unless they are wanted. */
  [System.Serializable]
  private class TextureSettings {
    public TextureWrapMode wrapMode = TextureWrapMode.Clamp;

    [Incrementable]
    [MinValue(1)]
    [MaxValue(4096)]
    [SerializeField]
    public int textureResolution = 128;

    public FilterMode filterMode = FilterMode.Bilinear;

    public bool useLinear = true;
  }

  private Texture2D _ramp = null;

  /* We use NonSerialized here so that during a Hot Reload, Unity doesn't serialize/deserialze
   * the entire array, which would be wasteful.  We regenerate the array if we need to update
   * the texture, but otherwise it can remain null. */
  [System.NonSerialized]
  private Color32[] _pixels = null;

  void Awake() {
    updateTexture();
  }

  /* This method ensures that the texture is updated in Edit Time whenever the user changes something,
   * resulting in a real-time feedback.  */
  void OnValidate() {
    updateTexture();
  }

  /* Shortcut that can be used to apply the ramp to a specific material property. */
  public void ApplyRampTo(Material material, string propertyName = "_MainTex") {
    material.SetTexture(propertyName, _ramp);
  }

  private Texture2D getTextureSafe() {
    if (_ramp == null || _ramp.width != _textureSettings.textureResolution) {
      _ramp = new Texture2D(_textureSettings.textureResolution, 1, TextureFormat.ARGB32, false, _textureSettings.useLinear);
      _ramp.hideFlags = HideFlags.HideAndDontSave;
    }

    return _ramp;
  }

  private void updateTexture() {
    if (_pixels == null || _pixels.Length != _textureSettings.textureResolution) {
      _pixels = new Color32[_textureSettings.textureResolution];
    }

    for (int i = 0; i < _pixels.Length; i++) {
      float percent = i / (_pixels.Length - 1.0f);
      _pixels[i] = _gradient.Evaluate(percent);
    }

    Texture2D curTex = getTextureSafe();
    curTex.wrapMode = _textureSettings.wrapMode;
    curTex.filterMode = _textureSettings.filterMode;
    curTex.name = "GeneratedRampTexture";
    curTex.SetPixels32(_pixels);
    curTex.Apply();

    Renderer r = GetComponent<Renderer>();
    if (r != null) {
      ApplyRampTo(r.sharedMaterial, _textureName);
    }
  }
}
