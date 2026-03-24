//--------------------------- 【描边】 - 法线扩张---------------------
Shader "Unlit/Outline"
{
// /---------------------------【属性】---------------------------
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _power ("Outline Width", Range(0, 0.2)) = 0.05      // 描边宽度（沿法线扩张距离）
        _lineColor ("Outline Color", Color) = (1,1,1,1)     // 描边颜色（白色）
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        // ===== 描边Pass（渲染背面，法线扩张）=====
        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "SRPDefaultUnlit" }

            Cull Front          // 剔除正面，只渲染背面作为描边
            ZWrite Off          // 不写入深度，让正面物体遮挡内部
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert_back
            #pragma fragment frag_back

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            float _power;
            half4 _lineColor;

            Varyings vert_back(Attributes input)
            {
                Varyings output;

                // 法线方向归一化
                float3 normal = normalize(input.normalOS);
                // 顶点沿法线扩张
                float3 expandedPosOS = input.positionOS.xyz + normal * _power;
                output.positionCS = TransformObjectToHClip(expandedPosOS);

                return output;
            }

            half4 frag_back(Varyings input) : SV_Target
            {
                return _lineColor;   // 白色描边
            }
            ENDHLSL
        }

        // ===== 主物体Pass（渲染正面，简单纹理）=====
        Pass
        {
            Name "Main"
            Tags { "LightMode" = "UniversalForward" }

            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert_front
            #pragma fragment frag_front

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            half4 _Color;

            Varyings vert_front(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                return output;
            }

            half4 frag_front(Varyings input) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                return texColor * _Color;
            }
            ENDHLSL
        }
    }
}