Shader "Custom/Jelly" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _JellyPower("Jelly Power", Range(0.0, 1.0)) = 0.5
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;
                float _JellyPower;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.rgb *= _Color.rgb;
                    col.a *= _Color.a;

                    // Jelly effect
                    col.rgb += sin(i.uv.x * 10 + _Time.y * 5) * _JellyPower;
                    col.rgb += sin(i.uv.y * 10 + _Time.y * 5) * _JellyPower;

                    return col;
                }
                ENDCG
            }
        }
}