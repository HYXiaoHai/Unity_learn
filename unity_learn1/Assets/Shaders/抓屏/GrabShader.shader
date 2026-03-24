Shader "Cunstom/GrabShader"
{
 Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        //设置渲染顺序为最后渲染，这样抓到的屏幕图形则是在所有物体渲染完成后的图像
        Tags { "RenderType"="Opaque" "Queue"="Overlay"}
        LOD 100
        //定义抓屏通道  {"_GrabPassTexture"}  表示抓屏的名称，如果声明的抓屏的名称，只需在shader中在声明一下，Unity会自动把抓屏的图形填充到我们声明的_GradaPss中
        //如果不填写抓屏的名称，那么Unity就会默认使用_GrabTexture进行保存
        GrabPass{"_GrabPassTexture"}

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            //抓屏Texture属性
            sampler2D _GrabPassTexture;
            fixed4 _GrabPassTexture_ST;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //因为采样得到的图片是反的所以我们这里翻转X轴 
                o.uv.x=1-o.uv.x;
                o.uv = TRANSFORM_TEX(v.uv, _GrabPassTexture);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_GrabPassTexture, i.uv);
                return col;
            }
            ENDCG
        }
    }

}
