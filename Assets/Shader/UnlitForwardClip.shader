// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32698,y:32535,varname:node_3138,prsc:2|emission-1211-RGB,clip-7174-OUT;n:type:ShaderForge.SFN_Tex2d,id:1211,x:32420,y:32635,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector4Property,id:8384,x:31523,y:32775,ptovrint:False,ptlb:pV,ptin:_pV,varname:node_8384,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:1000,v4:0;n:type:ShaderForge.SFN_Vector4Property,id:6760,x:31750,y:32935,ptovrint:False,ptlb:pN,ptin:_pN,varname:node_6760,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:1,v4:0;n:type:ShaderForge.SFN_FragmentPosition,id:6418,x:31523,y:32627,varname:node_6418,prsc:2;n:type:ShaderForge.SFN_Subtract,id:3106,x:31752,y:32726,varname:node_3106,prsc:2|A-6418-XYZ,B-8384-XYZ;n:type:ShaderForge.SFN_Normalize,id:2657,x:31979,y:32726,varname:node_2657,prsc:2|IN-3106-OUT;n:type:ShaderForge.SFN_Normalize,id:6586,x:31979,y:32935,varname:node_6586,prsc:2|IN-6760-XYZ;n:type:ShaderForge.SFN_Dot,id:1328,x:32186,y:32849,varname:node_1328,prsc:2,dt:0|A-2657-OUT,B-6586-OUT;n:type:ShaderForge.SFN_If,id:7174,x:32411,y:33024,varname:node_7174,prsc:2|A-1328-OUT,B-9059-OUT,GT-4557-OUT,EQ-4557-OUT,LT-1211-A;n:type:ShaderForge.SFN_Vector1,id:9059,x:32186,y:33045,varname:node_9059,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4557,x:32186,y:33118,varname:node_4557,prsc:2,v1:0;proporder:1211-8384-6760;pass:END;sub:END;*/

Shader "Shader Forge/UnlitForwardClip" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _pV ("pV", Vector) = (0,0,1000,0)
        _pN ("pN", Vector) = (0,0,1,0)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _pV;
            uniform float4 _pN;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_7174_if_leA = step(dot(normalize((i.posWorld.rgb-_pV.rgb)),normalize(_pN.rgb)),0.0);
                float node_7174_if_leB = step(0.0,dot(normalize((i.posWorld.rgb-_pV.rgb)),normalize(_pN.rgb)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_4557 = 0.0;
                clip(lerp((node_7174_if_leA*_MainTex_var.a)+(node_7174_if_leB*node_4557),node_4557,node_7174_if_leA*node_7174_if_leB) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _MainTex_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _pV;
            uniform float4 _pN;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_7174_if_leA = step(dot(normalize((i.posWorld.rgb-_pV.rgb)),normalize(_pN.rgb)),0.0);
                float node_7174_if_leB = step(0.0,dot(normalize((i.posWorld.rgb-_pV.rgb)),normalize(_pN.rgb)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_4557 = 0.0;
                clip(lerp((node_7174_if_leA*_MainTex_var.a)+(node_7174_if_leB*node_4557),node_4557,node_7174_if_leA*node_7174_if_leB) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
