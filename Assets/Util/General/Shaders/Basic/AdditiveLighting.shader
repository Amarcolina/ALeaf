Shader "HandyMan/General/AdditiveLighting" {
	Properties {
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma multi_compile LEAP_FORMAT_IR LEAP_FORMAT_RGB
		#include "Assets/LeapMotion/Resources/LeapCG.cginc"
		#pragma surface surf Lambert
		#pragma target 3.0

		struct Input {
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = float3(1,1,1);
			o.Emission = LeapColor(IN.screenPos);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
