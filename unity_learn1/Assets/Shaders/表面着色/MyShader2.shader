// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MyShader2"
{
    // 绑定到编辑器上的
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM//cg代码的开始
        #pragma surface surf Standard vertex:vert
        // #pragma surface surf Lambert fullforwardshadows
        #include "UnityCG.cginc"
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;//uv纹理的名称
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        //顶点入口
        void vert(inout appdata_full v)
        {
              v.vertex = UnityObjectToClipPos(v.vertex);
        }

        //表面着色的入口函数
        // void surf (Input IN, inout SurfaceOutput o)
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG//cg代码的结束
    }
    // 降级
    FallBack "Diffuse"
}
