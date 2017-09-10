// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UnlitTransparent" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}

  CGINCLUDE
  #include "UnityCG.cginc"
	sampler2D _MainTex;

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

	float4 frag(fragment_input input) : COLOR {
		return tex2D(_MainTex, input.uv);
	}
	ENDCG

	SubShader {
		Tags {"Queue"="Transparent"}

		Cull Off
		ZWrite Off
		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		ENDCG
		}
	}

	Fallback off
}
