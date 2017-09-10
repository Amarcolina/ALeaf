// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ALeaf/Glow" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}

  CGINCLUDE
  #include "UnityCG.cginc"

	struct fragment_input{
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

  fragment_input vert(appdata_img v) {
		fragment_input o;
		o.position = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}

  float4 _Color;

	float4 frag(fragment_input input) : COLOR {
		return (1 - input.uv.y) * _Color;
	}
	ENDCG

	SubShader {
		Tags {"Queue"="Geometry"}

		Cull Off
		ZWrite Off
		ZTest Off
		Blend One Zero

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		ENDCG
		}
	}

	Fallback off
}
