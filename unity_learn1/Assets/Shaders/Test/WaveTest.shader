Shader "Unlit/WaveTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Arange("Amplltute",float)= 1
        _Frenqunyc("Frequncy",float)=0.5
        _Speed("Speed",float)=0.5
         [HDR] _GlowIntensity("Glow Intensity", float) = 1.0
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
            float _Arange;
            float _Frenqunyc;
            float _Speed;
             float _GlowIntensity; // 声明新的属性
            v2f vert (appdata v)
            {
                v2f o;

                float timer = _Time.y*_Speed;

                float waver = _Arange*sin(timer + v.vertex.x*_Frenqunyc);
                // v.vertex.xyz = float3(v.vertex,v.vertex.y+waver,vertex.z);
                v.vertex.y = v.vertex.y+waver;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // float r = 0.5 + 0.5*cos(_Time.y + i.uv.x + 0);//r通道
				float3 col = 0.5 + 0.5*cos(_Time.y + i.uv.xyx + float3(0, 2, 4));
                float3 hdrCol = col * _GlowIntensity;
                return float4(hdrCol, 1.0);
            }
            ENDCG
        }
    }
}
