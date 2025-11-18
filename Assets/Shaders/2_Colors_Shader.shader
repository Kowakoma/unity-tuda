Shader "Unlit/2_Colors_Shader"
{
    Properties {
        _Color1 ("Color 1", Color) = (1,0,0,1)    // Красный
        _Color2 ("Color 2", Color) = (0,0,1,1)    // Синий
        _Split ("Split Position", Range(0,1)) = 0.5
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        
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
            
            fixed4 _Color1;
            fixed4 _Color2;
            float _Split;
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Если UV координата X меньше Split - Color1, иначе - Color2
                if (i.uv.x < _Split)
                    return _Color1;
                else
                    return _Color2;
            }
            ENDCG
        }
    }
}
