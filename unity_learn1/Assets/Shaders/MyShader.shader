Shader "Custom/MyShader"
{
    Properties//属性 可以在编辑器里面bind和修改
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM//插入CG代码
            struct my_struct
            {
                int a;
            };

            float sum(float a,float b)
            {
                return a+b;
            }

             #include "UnityCG.cginc"

            fixed4 _Color;//定义同样的变量
            sampler2D _MainTex;
            #pragma vertex my_vert //把my_vert作为顶点shader的入口
            #pragma fragment my_frag

            // 顶点着色器：输入对象空间顶点位置，输出裁剪空间位置（使用SV_POSITION）
            //获得上一个工位的参数；-》语义bind
            float4 my_vert(float4 pos : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(pos);
            }
            // 片段着色器：输出颜色（使用SV_Target）
            fixed4 my_frag(float2 uv:TEXCOORD0) : COLOR
            {
                // return fixed4(1.0, 0.0, 0.0, 1.0); 红色
                return _Color;
                
                // return tex2D(_MainTex,uv);
                // fixed4 color = tex2D(_MainTex,uv);
                // return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse" // 后备shader，当此shader无法运行时使用Diffuse
}