﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HandyMan/General/UnlitOverlayFade" {
	Properties {
		_Fade    ("Fade", Range(0, 1)) = 1
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags {"Queue"="Overlay+2"}

		Pass{

		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off

		CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _Fade;

            struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

            v2f vert (appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

            fixed4 frag(v2f i) : COLOR {
                float4 color = tex2D(_MainTex, i.uv);
				color.a *= _Fade;
				return color;
            }

		ENDCG
		}
	}
	Fallback "Legacy Shaders/VertexLit"
}
