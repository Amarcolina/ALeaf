Shader "HandyMan/Hands/HandOutline" {

	/* Renders a nice outline around a mesh using the stencil buffer
	 * works a lot nicer than using extruded meshes alone, like how
	 * unity does it (but doesnt work in deffered).  This also has
	 * a pass at the start for occlusion
	 */

	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_Radius ("Outline radius", Range (0, 0.05)) = 0
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float _Radius;
	uniform float4 _OutlineColor;

	//Hand extruded by _Radius amount, used to mask the outline
	v2f vertHandRadius(appdata v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Radius;
		o.color = _OutlineColor;
		return o;
	}

	//Hand extruded by (_Radius + _Outline) amount, used when drawing the outline
	v2f vertOutline(appdata v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * (_Outline + _Radius);
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

	SubShader {
		Tags { "Queue" = "Overlay" }

		Pass {
			Name "EXTRUDED_HAND"
		    Cull Off
			ZWrite Off
			ZTest Off
			Blend Zero One

			Stencil{
				Ref 23
				Comp always
				Pass replace
				Fail replace
			}

			CGPROGRAM
			#pragma vertex vertHandRadius
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return half4(0,0,0,0); }
			ENDCG
		}

		Pass {
			Name "OUTLINE"
			Cull Off
			ZWrite On
			Blend SrcAlpha One

			Stencil{
				Ref 23
				Comp NotEqual
				Pass replace
			}

			CGPROGRAM
			#pragma vertex vertOutline
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
	}
}
