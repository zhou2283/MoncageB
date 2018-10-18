Shader "Hidden/SC Post Effects/Colorize"
{
	HLSLINCLUDE

	#include "../../../../Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	TEXTURE2D_SAMPLER2D(_ColorRamp, sampler_ColorRamp);

	float _Intensity;

	float4 Frag(VaryingsDefault i) : SV_Target
	{

		float4 screenColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);

		float luminance = (screenColor.r * 0.3 + screenColor.g * 0.59 + screenColor.b * 0.11);

		float4 ramp = SAMPLE_TEXTURE2D(_ColorRamp, sampler_ColorRamp, float2(luminance, 0));
	
		float3 color = lerp(screenColor.rgb, ramp.rgb, _Intensity * ramp.a);

		return float4(color, screenColor.a);
	}

		ENDHLSL

		SubShader
	{
		Cull Off ZWrite Off ZTest Always

			Pass
		{
			HLSLPROGRAM

			#pragma vertex VertDefault
			#pragma fragment Frag

			ENDHLSL
		}
	}
}