// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mask/BeCulling"
{
	Properties
	{
	    _InnerColor("InnerColor",color)=(1,1,1,1)
	    _InnerBright("InnerBright",range(0,1))=0.2
	    _Alpha("Alpha",range(0,1))=0.7
	    _Spec("Spec",  range(0,1) )=0.5
		_MainTex ("Texture", 2D) = "white" {}
		_B("B",range(0,1))=1
		_S("S",range(0,1))=1
		_P("P",range(0,5))=1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "queue"="transparent" }
		LOD 100

		//inside face
		/*
		Pass
		{
	
			cull front
			alphatest greater 0
			blend srcalpha oneminussrcalpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			#include "UnityCG.cginc"
			struct v2f
			{
				float2 uv:TEXCOORD0;
				float3 L: TEXCOORD3;
				float3 V: TEXCOORD4;
				float3 N: TEXCOORD5;
				float4 vertex : SV_POSITION;
				float3 wpos:TEXCOORD6;
			};

			float3 pV;
			float3 pN;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _B,_S,_P;
			float4 _InnerColor;
			float _InnerBright,_Alpha,_Spec;
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 i= WorldSpaceViewDir(v.vertex);
				o.V= normalize(i);
				float3 n= mul(float4( -v.normal,0), unity_WorldToObject).xyz;
				o.N=normalize(n);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.L= WorldSpaceLightDir(v.vertex);
				o.L= normalize(o.L);
				o.wpos= mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col=tex2D(_MainTex, i.uv);	


				float diff=  max(0, dot(i.L,i.N));
				float spec= pow( max(0, dot(i.N, normalize(i.L+i.V))),64);
					float fresnel= _B+ _S * pow( 1+ dot(-normalize(i.V+i.L),i.N),_P);
				col= col * diff *_LightColor0 * _InnerColor+  _LightColor0* spec*_InnerColor*_Spec;
				col+=_InnerBright;

				//打开这句就为内部面计算Fresnel
			  //  col = lerp(col, _LightColor0, fresnel);


				float3 p= normalize( i.wpos-pV);
				float3 n= normalize(pN);
				float angle= dot(p,n);
				col.a= _Alpha;
				if(angle>=0)
					col.a= 0;		
				
				return col;
			}
			ENDCG
		}
		*/
		//==============================
		Pass
		{

			alphatest greater 0
			blend srcalpha oneminussrcalpha
			tags{"lightmode"="forwardbase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			#include "UnityCG.cginc"
			struct v2f
			{
				float2 uv:TEXCOORD0;
				float3 L: TEXCOORD3;
				float3 V: TEXCOORD4;
				float3 N: TEXCOORD5;
				float4 vertex : SV_POSITION;
				float3 wpos:TEXCOORD6;
			};
			float3 pV;
			float3 pN;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _B,_S,_P;
			float _Alpha,_Spec;
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 i= WorldSpaceViewDir(v.vertex);
				o.V= normalize(i);
				float3 n= mul(float4( v.normal,0), unity_WorldToObject).xyz;
				o.N=normalize(n);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.L= WorldSpaceLightDir(v.vertex);
				o.L= normalize(o.L);
				o.wpos= mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col=tex2D(_MainTex, i.uv);	


				float diff=  max(0, dot(i.L,i.N));
				float spec= pow( max(0, dot(i.N, normalize(i.L+i.V))),64);
					float fresnel= _B+ _S * pow( 1+ dot(-normalize(i.V+i.L),i.N),_P);
				col= col * diff *_LightColor0+  _LightColor0* spec*_Spec;

			      col = lerp(col, _LightColor0, fresnel);
				col.a= _Alpha;

				float3 p= normalize( i.wpos-pV);
				float3 n= normalize(pN);
				float angle= dot(p,n);
			
				if(angle>=0)
					col.a= 0;		
				return col;
			}
			ENDCG
		}

	

	
	}
}
