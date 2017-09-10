Shader "HandyMan/Passthrough/BlueThreshold" {
	Properties {
		_MinThreshold    ("Min Threshold", Float) = 0.2
		_MaxThreshold    ("Max Threshold", Float) = 0.3
		_Fade            ("Fade", Float)      = 0.3	
	}

	SubShader {
		Tags {"Queue"="Transparent+500" "IgnoreProjector"="True" "RenderType"="Fooopy"}

		Lighting Off
		ZTest Always
		ZWrite Off

		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM
		#pragma multi_compile LEAP_FORMAT_IR LEAP_FORMAT_RGB
		#include "Assets/LeapMotion/Resources/LeapCG.cginc"
		#include "UnityCG.cginc"

		#pragma vertex vert
		#pragma fragment frag
		
		uniform float    _MaxThreshold;
		uniform float    _MinThreshold;
		uniform float    _Fade;

		struct frag_in{
			float4 position : SV_POSITION;
			float4 screenPos  : TEXCOORD0;
		};

		frag_in vert(appdata_img v){
			frag_in o;
			o.position = mul(UNITY_MATRIX_MVP, v.vertex);
			o.screenPos = ComputeScreenPos(o.position);
			return o;
		}

		float4 frag (frag_in i) : COLOR {
			float3 color = LeapRawColor(i.screenPos);
			float alpha = _Fade * smoothstep(_MinThreshold, _MaxThreshold, color.b);
			color = pow(color, _LeapGammaCorrectionExponent);
			return float4(color, alpha);
		}

		ENDCG
		}
	} 
	Fallback "Unlit/Texture"
}