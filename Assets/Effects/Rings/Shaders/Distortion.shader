// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Distortion/DistortingGrabPass" 
{
    Properties 
    {
        _Mask("Mask", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 1)) = 0
    }
    SubShader 
    {
        GrabPass { "_GrabTexture" }
        
        Pass 
        {
            Tags { "Queue"="Transparent" }
            
            cull off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct v2f 
            {
                half4 pos : SV_POSITION;
                half4 grabPos : TEXCOORD0;
            };
            
            sampler2D _GrabTexture;
            sampler2D _Mask;
            half _Intensity;
            
            v2f vert(appdata_base v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }
            
            half4 frag(v2f i) : COLOR 
            {
                float4 mask = tex2D(_Mask, float2(0, 0));

                // i.grabPos.x += sin((_Time.y + i.grabPos.y) * _Intensity)/20;
                i.grabPos.x += sin((mask + i.grabPos.y) * _Intensity);
                fixed4 color = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos));
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}