Shader "Custom/InvisibleWithShadows_HDRP"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,0)
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // Shadow caster pass to ensure it can receive and cast shadows
        Pass
        {
            Tags { "LightMode" = "ShadowCaster" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.high-definition/ShaderLibrary/Core.hlsl"

        // Vertex and Fragment shaders
        struct Attributes
        {
            float4 vertex : POSITION;
        };

        struct Varyings
        {
            float4 pos : SV_POSITION;
        };

        Varyings vert(Attributes v)
        {
            Varyings o;
            o.pos = UnityObjectToClipPos(v.vertex);
            return o;
        }

        half4 frag() : SV_Target
        {
            return half4(0, 0, 0, 0); // Invisible but still casts shadow
        }
        ENDCG
    }
    }
}
