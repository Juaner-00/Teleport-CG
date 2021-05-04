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
                half4 uv_Mask : TEXCOORD1;
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
                half4 mask = tex2D(_Mask, i.uv_Mask.xy);
                half4 original = tex2D(_GrabTexture, i.grabPos.xy);
                // i.grabPos.x += sin((_Time.y + i.grabPos.y) * _Intensity)/20;
                i.grabPos.x += i.grabPos.y * _Intensity * mask.x;
                // i.grabPos.x += sin((i.grabPos.y) * _Intensity);
                half4 color = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos));
                return lerp (original, color, mask);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}