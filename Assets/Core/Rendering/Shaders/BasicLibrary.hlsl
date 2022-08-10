#pragma once

#include "Common.hlsl"

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);

CBUFFER_START(UnityPerMaterial)
float4 _Color;
float4 _MainTex_ST;
CBUFFER_END

CBUFFER_START(_CustomLight)
float3 _DirectionalLightColor;
float3 _DirectionalLightDirection;
CBUFFER_END

struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
    float2 baseUV : TEXCOORD0;
};

struct Varyings
{
    float4 positionCS : SV_POSITION;
    float3 normalWS : VAR_NORMAL;
    float2 baseUV : VAR_BASE_UV;
};

float CalcLight(float3 normalWS)
{
    return dot(normalWS, _DirectionalLightDirection);
}

Varyings BasicVertex(Attributes IN)
{
    Varyings OUT;
    float3 positionWS = TransformObjectToWorld(IN.positionOS);
    OUT.positionCS = TransformWorldToHClip(positionWS);
    OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
    OUT.baseUV = IN.baseUV * _MainTex_ST.xy + _MainTex_ST.zw;
    return OUT;
}

float4 BasicFragment(Varyings IN) : SV_TARGET
{
    float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.baseUV);
    tex.rgb *= _DirectionalLightColor;
    return _Color * tex * CalcLight(IN.normalWS);
}
