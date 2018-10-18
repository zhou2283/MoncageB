Shader "Hidden/SC Post Effects/Light Streaks"
{
	HLSLINCLUDE

	#include "../../../../Shaders/StdLib.hlsl"
	#include "../../../../Shaders/Sampling.hlsl"
	#include "../../../Editor/ShaderLibraries/SCPE.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	TEXTURE2D_SAMPLER2D(_BloomTex, sampler_BloomTex);
	float4 _MainTex_TexelSize;
	float _SampleDistance;
	float _Threshold;
	float4 _BlurOffsets;
	float _Blur;
	float _Intensity;

	float3 FragLuminanceDiff(VaryingsDefault i) : SV_Target
	{
		float3 luminance = LuminanceThreshold(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo), _Threshold).rgb;
		luminance *= _Intensity;

		return luminance.rgb;
	}

	float4 FragBlurBox(VaryingsDefault i) : SV_Target
	{
		return DownsampleBox4Tap(TEXTURE2D_PARAM(_MainTex, sampler_MainTex), i.texcoord, _BlurOffsets.xy).rgba;
	}

	struct v2fGaussian {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;

		float4 uv01 : TEXCOORD1;
		float4 uv23 : TEXCOORD2;
		float4 uv45 : TEXCOORD3;
	};

	v2fGaussian VertGaussian(AttributesDefault v) {
		v2fGaussian o;
		o.pos = float4(v.vertex.xy, 0, 1);

		o.uv.xy = TransformTriangleVertexToUV(o.pos.xy);

#if UNITY_UV_STARTS_AT_TOP
		o.uv = o.uv * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
		//UNITY_SINGLE_PASS_STEREO
		o.uv = TransformStereoScreenSpaceTex(o.uv, 1.0);

		o.uv01 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1);
		o.uv23 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1) * 2.0;
		o.uv45 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1) * 6.0;

		return o;
	}

	float4 FragBlurGaussian(v2fGaussian i) : SV_Target
	{
		half4 color = float4(0, 0, 0, 0);

		color += 0.40 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
		color += 0.15 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv01.xy);
		color += 0.15 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv01.zw);
		color += 0.10 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv23.xy);
		color += 0.10 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv23.zw);
		color += 0.05 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv45.xy);
		color += 0.05 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv45.zw);

		return color;
	}

	float4 FragBlend(VaryingsDefault i) : SV_Target
	{
		float4 original = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);
		float3 bloom = SAMPLE_TEXTURE2D(_BloomTex, sampler_BloomTex, i.texcoordStereo).rgb;

		return float4(original.rgb + bloom, original.a);
	}

	float4 FragDebug(VaryingsDefault i) : SV_Target
	{
		return SAMPLE_TEXTURE2D(_BloomTex, sampler_BloomTex, i.texcoordStereo);
	}

		ENDHLSL

		SubShader
	{
		Cull Off ZWrite Off ZTest Always

			Pass //0
		{
			HLSLPROGRAM

#pragma vertex VertDefault
#pragma fragment FragLuminanceDiff

			ENDHLSL
		}
			Pass //1
		{
			HLSLPROGRAM

#pragma vertex VertDefault
#pragma fragment FragBlurBox

			ENDHLSL
		}
			Pass //2
		{
			HLSLPROGRAM

#pragma vertex VertGaussian
#pragma fragment FragBlurGaussian

			ENDHLSL
		}
			Pass //3
		{
			HLSLPROGRAM

#pragma vertex VertDefault
#pragma fragment FragBlend

			ENDHLSL
		}
			Pass //4
		{
			HLSLPROGRAM

#pragma vertex VertDefault
#pragma fragment FragDebug

			ENDHLSL
		}
	}
}