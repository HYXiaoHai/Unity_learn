Shader "Unlit/Loading"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Speed",float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        // Blend SrcAlpha OneMinusSrcAlpha
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
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 tmpUV = i.uv;
                tmpUV-=float2(0.5,0.5);//平移到原点
                if(length(tmpUV)>0.5)
                {
                    return fixed4(0,0,0,0);
                }
                float2 finalUV =(0,0);
                //旋转
                float angle = _Time.y*_Speed;
                finalUV.x = tmpUV.x*cos(angle)-tmpUV.y*sin(angle);
                finalUV.y = tmpUV.x*sin(angle)+tmpUV.y*cos(angle);

                //平移回去
                finalUV += float2(0.5,0.5);
                fixed4 col = tex2D(_MainTex,finalUV);
                return col;
            }
            ENDCG
        }
    }
}
