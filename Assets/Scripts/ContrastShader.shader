Shader "Hidden/ContrastShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Factor("Factor",float)=0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
  
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fasatest

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Factor;

            fixed4 frag (v2f_img i) : SV_Target
            {
             
                float4 main = tex2D(_MainTex, i.uv);
                float4 output = float4(0,0,0,1);

                /*
                output.r= main.b;
                output.g= main.g;
                output.b= main.r;*/

                //output = main-0.25; // Menos brillo
                //output = main+0.25; //Más brillo
                //output = pow(main,_Factor); //Gamma Correction
                output = (main-0.15)*1.45; //Contraste
                    
                return lerp(main,output,_Factor);
                //return output;
            }
            ENDCG
        }
    }
}
