Shader "Custom/Leaf" {
	Properties {
		_MainTex   ("Albedo (RGB)", 2D) = "white" {}
    _Emission  ("Emission", Range(0, 1)) = 0.4
    _Deform    ("Deform Amount", Float) = 0
    _XScale    ("X Scale", Float) = 1
    _YScale    ("Y Scale", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

    float _Emission;
    float _Deform;
    float _XScale;
    float _YScale;

    void vert (inout appdata_full v) {
      v.vertex.z += v.color.r * _Deform * length(v.vertex.xy * float2(_XScale, _YScale));
    }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			o.Albedo = c.rgb;
      o.Emission = c.rgb * _Emission;
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
