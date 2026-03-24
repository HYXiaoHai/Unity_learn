Shader "Custom/BlendShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Color("Color",Color)=(1.0,0.0,0.0,1.0)
        _Alpha("Alpha",float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"= "Transparent+1" }
        // Tags { "DisableBatching" = "False" }防止 Unity 的动态批处理或静态批处理将该物体与其它物体合并，从而保留模型的本地坐标空间。常用于在顶点着色器中做基于本地坐标的动画（如顶点偏移、草地摆动）。
        // Tags { "ForceNoShadowCasting" = "False }强制该物体不投射阴影，即使开启了阴影投射功能。
        // Tags { "IgnoreProjector" = "值" }控制物体是否受 Projector 组件的影响（Projector 常用于实现贴花、投影纹理等）。
        // Tags { "CanUseSpriteAtlas" = "值" }告知 Unity 该 Shader 是否能与精灵图集（Sprite Atlas）兼容。主要用于 2D 精灵。
        // Tags { "PreviewType" = "值" }仅在材质编辑器的预览窗口中生效，决定使用什么形状来预览材质效果。

        LOD 100

        Pass
        {
            // ColorMask RGBA//通道遮罩
            // Color(1.0,1.0,1.0,1.0);

            Blend SrcAlpha OneMinusSrcAlpha
            // 变量要加上[]

            AlphaTest Never [_Alpha]//alpha测试
            // Cull Off关闭面剔除
            // Cull Front不绘制面对摄像机的面
            Cull Back//不绘制背对摄像机的面
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
            float4 _Color;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
               fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
