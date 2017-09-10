Shader "HandyMan/General/RimGlow" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_GlowColor ("Glow Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_GlowPower("Glow Power", Range(0, 400)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _GlowColor;
		half _GlowPower;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			float rim = saturate(dot(normalize(IN.viewDir), o.Normal));
			float rim1 = pow(rim, 4);
			float rim2 = pow(1.0 - rim, 4);

			o.Emission = _GlowColor * rim1 * rim2 * _GlowPower;

			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
