using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ColorUtil {

    public static Color transparent(this Color c) {
        return c.atAlpha(0.0f);
    }

    public static Color opaque(this Color c) {
        return c.atAlpha(1.0f);
    }

    public static Color atAlpha(this Color c, float alpha) {
        return new Color(c.r, c.g, c.b, alpha);
    }

    public static Color toGammaSpace(this Color c) {
      float r = Mathf.LinearToGammaSpace(c.r);
      float g = Mathf.LinearToGammaSpace(c.g);
      float b = Mathf.LinearToGammaSpace(c.b);
      return new Color(r, g, b, c.a);
    }

    public static Color toLinearSpace(this Color c) {
      float r = Mathf.GammaToLinearSpace(c.r);
      float g = Mathf.GammaToLinearSpace(c.g);
      float b = Mathf.GammaToLinearSpace(c.b);
      return new Color(r, g, b, c.a);
    }

    public static Color HSVToRGB(this Vector3 hsv) {
        if (hsv.y == 0f)
            return new Color(hsv.z, hsv.z, hsv.z);
        else if (hsv.x == 0f)
            return Color.black;
        else {
            Color col = Color.black;
            float Hval = hsv.x * 6f;
            int sel = Mathf.FloorToInt(Hval);
            float mod = Hval - sel;
            float v1 = hsv.z * (1f - hsv.y);
            float v2 = hsv.z * (1f - hsv.y * mod);
            float v3 = hsv.z * (1f - hsv.y * (1f - mod));
            switch (sel + 1) {
                case 0:
                    col.r = hsv.z;
                    col.g = v1;
                    col.b = v2;
                    break;
                case 1:
                    col.r = hsv.z;
                    col.g = v3;
                    col.b = v1;
                    break;
                case 2:
                    col.r = v2;
                    col.g = hsv.z;
                    col.b = v1;
                    break;
                case 3:
                    col.r = v1;
                    col.g = hsv.z;
                    col.b = v3;
                    break;
                case 4:
                    col.r = v1;
                    col.g = v2;
                    col.b = hsv.z;
                    break;
                case 5:
                    col.r = v3;
                    col.g = v1;
                    col.b = hsv.z;
                    break;
                case 6:
                    col.r = hsv.z;
                    col.g = v1;
                    col.b = v2;
                    break;
                case 7:
                    col.r = hsv.z;
                    col.g = v3;
                    col.b = v1;
                    break;
            }
            col.r = Mathf.Clamp(col.r, 0f, 1f);
            col.g = Mathf.Clamp(col.g, 0f, 1f);
            col.b = Mathf.Clamp(col.b, 0f, 1f);
            return col;
        }
    }

    public static Vector3 RGBToHSV(this Color rgbColor) {
        float H, S, V;
        if (rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r) {
            RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
        } else {
            if (rgbColor.g > rgbColor.r) {
                RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
            } else {
                RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
            }
        }
        return new Vector3(H, S, V);
    }

    private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V) {
        V = dominantcolor;
        if (V != 0f) {
            float num = 0f;
            if (colorone > colortwo) {
                num = colortwo;
            } else {
                num = colorone;
            }
            float num2 = V - num;
            if (num2 != 0f) {
                S = num2 / V;
                H = offset + (colorone - colortwo) / num2;
            } else {
                S = 0f;
                H = offset + (colorone - colortwo);
            }
            H /= 6f;
            if (H < 0f) {
                H += 1f;
            }
        } else {
            S = 0f;
            H = 0f;
        }
    }
}
