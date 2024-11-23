Shader "Unlit/HeatWaves"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DisplacementTexture ("DisplacementTexture", 2D) = "white" {}
        _DisplacementPower ("Displacement", Vector) = (0, 0, 0, 0)
        _Speed ("Displacement speed",Vector) = (0, 0, 0, 0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DisplacementTexture;
            float4 _DisplacementPower;
            float4 _Speed;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 uvOffset  = tex2D(_DisplacementTexture, frac(i.uv + _Time.x * _Speed.xy)) * 2 - 1;
                i.uv.x += uvOffset.r * _DisplacementPower.x;
                i.uv.y += uvOffset.r * _DisplacementPower.y;
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
