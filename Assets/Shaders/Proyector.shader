Shader "Custom/Proyector"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Factor ("Factor", Range(0,1)) = 0.0
        _Factor2 ("Factor", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent"  "RenderType"="Transparent"  }


        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        float _Factor;
        float _Factor2;
        

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            float4 cLerp = lerp(c.r,c.g,_Factor);
            float4 cLerp2 = lerp(cLerp.g,cLerp.b,_Factor2);


            o.Alpha = c.a;
            o.Albedo = cLerp2 ;    
        }
        ENDCG
    }
    FallBack "Diffuse"
}
