// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ALeaf/Downsample" {
  Properties { _MainTex ("", any) = "" {} }
  CGINCLUDE
  #include "UnityCG.cginc"

  sampler2D _MainTex;
  half4 _MainTex_TexelSize;

  struct v2f {
    float4 pos : SV_POSITION;
    half2 taps[4] : TEXCOORD1; 
  };

  v2f vert( appdata_img v ) {
    v2f o; 
    o.pos = UnityObjectToClipPos(v.vertex);

    o.taps[0] = v.texcoord + float2(0, 0);
    o.taps[1] = v.texcoord + float2(_MainTex_TexelSize.x, 0);
    o.taps[2] = v.texcoord + float2(0, _MainTex_TexelSize.y);
    o.taps[3] = v.texcoord + float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y);
    return o;
  }

  float4 frag(v2f i) : SV_Target {
    float4 n0 = tex2D(_MainTex, i.taps[0]);
    float4 n1 = tex2D(_MainTex, i.taps[1]);
    float4 n2 = tex2D(_MainTex, i.taps[2]);
    float4 n3 = tex2D(_MainTex, i.taps[3]);

    return (n0 + n1 + n2 + n3) / 4.0;
  }

  ENDCG
  SubShader {
    ZTest Always Cull Off ZWrite Off

     Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        ENDCG
      }
  }
  Fallback off
}
