Shader "Debug/TFR"
{
    Properties
    {
        _Size ("Size", float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType" = "Transparent"}
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha 
        
        CGINCLUDE

        #include "UnityCG.cginc"
        
        struct appdata
        {
            float4 vertex : POSITION;
            fixed4 color : COLOR;
        };

        struct v2f
        {
            fixed4 color : COLOR;
            float4 vertex : SV_POSITION;
        };

        float _Size;

        v2f vert (appdata v)
        {
            v2f o;
            float x = v.vertex.y;
            float oneMinusSize = 1 - _Size;
            if(v.vertex.y <= 1)
            {
                v.vertex.y = _Size + (v.vertex.y - _Size) * 0.5; 
                x = (x - _Size) / oneMinusSize;
            }
            else
            {
                x = (x - 1 - _Size) / oneMinusSize;
            }
            o.color = lerp(fixed4(0,1,0,1), fixed4(1,0,0,1), x);
            o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }

        v2f vert2 (appdata v)
        {
            v2f o;
            o.color = v.color;
            o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }
        
        fixed4 frag (v2f i) : SV_Target
        {
            return i.color;
        }

        ENDCG

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert2
            #pragma fragment frag
            ENDCG
        }       
    }
}