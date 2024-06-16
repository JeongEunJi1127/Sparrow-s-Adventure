// Shader "Toon/SoftSurface"
// {
//     Properties
//     {
//         _Color ("Color", Color) = (0.4,0.4,0.4,1)
//         _MainTex ("Albedo (RGB)", 2D) = "white" {}
//         _Emission ("Emission", Range (0,1)) = 0.5
//     }
//     SubShader
//     {
//         Tags { "RenderType"="Opaque" }
//         LOD 100

//         CGPROGRAM
//         #pragma surface surf Lambert
//         #pragma target 3.0

//         struct Input
//         {
//             float2 uv_MainTex;
//         };

//         sampler2D _MainTex;
//         fixed4 _Color;
//         fixed _Emission;

//         void surf (Input IN, inout SurfaceOutput o)
//         {
//             fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//             o.Albedo = c.rgb;
//             // Re-use the same texture on emission to give the soft look
//             o.Emission = tex2D (_MainTex, IN.uv_MainTex) * _Emission;
//             o.Alpha = c.a;
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }
Shader "Toon/SoftSurfaceURP"
{
    Properties
    {
        _Color ("Color", Color) = (0.4,0.4,0.4,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Emission ("Emission", Range (0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _Emission;
        CBUFFER_END

        struct Attributes
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 positionHCS : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        Varyings Vert(Attributes input)
        {
            Varyings output;
            UNITY_SETUP_INSTANCE_ID(input);
            output.positionHCS = TransformObjectToHClip(input.positionOS);
            output.uv = input.uv;
            return output;
        }

        half4 Frag(Varyings input) : SV_Target
        {
            half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv) * _Color;
            half3 emission = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv).rgb * _Emission;
            return half4(c.rgb + emission, c.a);
        }
        ENDHLSL

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _REFLECTION_PROBE
            #pragma multi_compile _ _LOCAL_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
