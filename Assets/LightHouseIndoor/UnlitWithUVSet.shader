// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32709,y:32638,varname:node_3138,prsc:2|emission-8141-OUT;n:type:ShaderForge.SFN_TexCoord,id:9346,x:31905,y:32615,varname:node_9346,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3661,x:32095,y:32615,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3661,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e31b6f81f412a374b8cd82f09644077f,ntxv:0,isnm:False|UVIN-9346-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7048,x:32095,y:32810,ptovrint:False,ptlb:AOTex,ptin:_AOTex,varname:node_7048,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7fe0d5ace03abfe44a9d7b6b3750cb2c,ntxv:0,isnm:False|UVIN-6278-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6278,x:31905,y:32810,varname:node_6278,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Color,id:9054,x:32095,y:33002,ptovrint:False,ptlb:AOTint,ptin:_AOTint,varname:node_9054,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:9523,x:32095,y:32440,ptovrint:False,ptlb:BaseTint,ptin:_BaseTint,varname:_AOTint_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5534,x:32309,y:32525,varname:node_5534,prsc:2|A-9523-RGB,B-3661-RGB;n:type:ShaderForge.SFN_Multiply,id:8141,x:32500,y:32738,varname:node_8141,prsc:2|A-5534-OUT,B-3538-OUT;n:type:ShaderForge.SFN_Add,id:8583,x:32302,y:32901,varname:node_8583,prsc:2|A-7048-RGB,B-9054-RGB;n:type:ShaderForge.SFN_Clamp01,id:3538,x:32478,y:32901,varname:node_3538,prsc:2|IN-8583-OUT;proporder:3661-7048-9054-9523;pass:END;sub:END;*/

Shader "Shader Forge/UnlitWithUVSet" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _AOTex ("AOTex", 2D) = "white" {}
        _AOTint ("AOTint", Color) = (0,0,0,1)
        _BaseTint ("BaseTint", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _AOTex; uniform float4 _AOTex_ST;
            uniform float4 _AOTint;
            uniform float4 _BaseTint;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _AOTex_var = tex2D(_AOTex,TRANSFORM_TEX(i.uv1, _AOTex));
                float3 emissive = ((_BaseTint.rgb*_MainTex_var.rgb)*saturate((_AOTex_var.rgb+_AOTint.rgb)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
