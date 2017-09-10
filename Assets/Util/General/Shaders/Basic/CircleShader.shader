// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HandyMan/General/CircleShader" {

	/* Renders a circle across UV coordinates based on a texture ramp
	 * This allows the circle to be scaled without modifying the
	 * width, which looks nicer.
	 */

	Properties {
		_Ramp   ("Texture Ramp", 2D) = "white" {}
		_Radius ("Radius", Range(0, 1)) = 0
		_Width  ("Width", Range(0, 1)) = 1
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	#define MSIZE 5

	uniform sampler2D _Ramp;
	uniform float _Radius;
	uniform float _Width;

	struct fragment_input{
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	fragment_input vert(appdata_img v) {
		fragment_input o;
		o.position = UnityObjectToClipPos(v.vertex);
		o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
		return o;
	}

	float4 frag(fragment_input input) : COLOR {
		float distFromCenter = length(input.uv - float2(0.5, 0.5)) * 2;

		float rampValue = saturate((distFromCenter - (_Radius - _Width)) / _Width);

		return 7 * tex2D(_Ramp, float2(rampValue, 0));
	}
	ENDCG

	SubShader {
		Tags {"Queue"="Transparent+500"}

		Cull Off
		ZWrite Off
		ZTest Off
		Blend SrcAlpha One

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		ENDCG
		}
	}

	Fallback "Unlit/Texture"
}
