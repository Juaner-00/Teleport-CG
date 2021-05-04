// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Distortion/DistortingGrabPass" 
{
    Properties 
    {
        _Mask("Mask", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 0.2)) = 0
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
                half4 grabPos : TEXCOORD1;
                half4 uv_Mask : TEXCOORD0;
            };
            
            sampler2D _GrabTexture;
            sampler2D _Mask;
            half _Intensity;
            
            v2f vert(appdata_full v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv_Mask = v.texcoord;
                o.grabPos = ComputeGrabScreenPos(o.pos);

                return o;
            }
            
            half4 frag(v2f i) : COLOR 
            {
                half4 mask = tex2D(_Mask, i.uv_Mask.xy);
                half4 original = tex2D(_GrabTexture, i.grabPos.xy);
                
                half4 originalColor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos));
                
                // i.grabPos.x += sin((_Time.y + i.grabPos.y) * _Intensity * mask)/20;
                i.grabPos.x += _Intensity * mask;
                // i.grabPos.x +=  lerp(i.uv_Mask, mask.x, _Intensity);

                // i.grabPos.x += sin((i.grabPos.y) * _Intensity);
                half4 color = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos));
                // return lerp(originalColor, color, mask);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}