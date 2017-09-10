using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MaterialPropertyAnimator : MonoBehaviour {
    public List<MaterialFloatRamp> floatProperties = new List<MaterialFloatRamp>();
    public List<MaterialColorRamp> colorProperties = new List<MaterialColorRamp>();

    [MinValue(0)]
    public float speed = 1.0f;

    private float enableTime = 0.0f;

    public abstract class MaterialRamp {
        public string materialProperty;
        public AnimationCurve curve;

        protected Material mat;

        public bool isFinished(float time) {
            Keyframe keyframe = curve[curve.length - 1];
            if (time < keyframe.time) {
                return false;
            }
            if (curve.postWrapMode != WrapMode.Clamp && curve.postWrapMode != WrapMode.ClampForever) {
                return false;
            }
            return true;
        }

        public void setMaterial(Renderer r) {
            foreach (Material m in r.materials) {
                if (m.HasProperty(materialProperty)) {
                    mat = m;
                    break;
                }
            }
        }

        public abstract void setProperty(float time);
    }

    [System.Serializable]
    public class MaterialFloatRamp : MaterialRamp {
        public override void setProperty(float time) {
            mat.SetFloat(materialProperty, curve.Evaluate(time));
        }
    }

    [System.Serializable]
    public class MaterialColorRamp : MaterialRamp{
        public Color startColor;
        public Color endColor;

        public override void setProperty(float time) {
            mat.SetColor(materialProperty, Color.Lerp(startColor, endColor, curve.Evaluate(time)));
        }
    }

    private IEnumerable<MaterialRamp> ramps() {
        foreach (MaterialFloatRamp f in floatProperties) {
            yield return f;
        }
        foreach (MaterialColorRamp f in colorProperties) {
            yield return f;
        }
    }

    public void Awake() {
        foreach (MaterialRamp ramp in ramps()) {
            ramp.setMaterial(GetComponent<Renderer>());
        }

        Update();
    }

    public void OnEnable() {
        enableTime = Time.time;
    }

    void Update() {
        foreach (MaterialRamp ramp in ramps()) {
            ramp.setProperty(speed * (Time.time  - enableTime));
        }
    }

    
}
