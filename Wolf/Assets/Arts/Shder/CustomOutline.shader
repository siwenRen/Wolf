// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
///*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:Unlit/Texture,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32394,y:32755|emission-4-RGB,olwid-3-OUT,olcol-2-RGB;n:type:ShaderForge.SFN_Color,id:2,x:32794,y:33027,ptlb:outLineColor,ptin:_outLineColor,glob:False,c1:0.183925,c2:0.07720587,c3:0.75,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3,x:32794,y:32935,ptlb:outLineSize,ptin:_outLineSize,glob:False,v1:0.1;n:type:ShaderForge.SFN_Tex2d,id:4,x:32794,y:32739,ptlb:MainTex,ptin:_MainTex,ntxv:0,isnm:False;proporder:3-2-4;pass:END;sub:END;*/

Shader "Custom/Outline" {
    Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	
		// silhouette and outline
		_outLineSize ("outLineSize", Range(0, 1)) = 0.02
        _outLineColor ("outLineColor", Color) = (0.40392157,0.15686275,0.15686275,0)
		
		// change color
		_ChangeColor ("ChangeColor", Color) = (1,1,1,1)
        _Blend("_Blend", Range(0,1) ) = 0
	}
 
	CGINCLUDE
	#include "UnityCG.cginc"
	struct appdata {
		half4 vertex : POSITION;
		half3 normal : NORMAL;
		half4 texcoord : TEXCOORD0;
	};
	 
	struct v2f {
		half4 pos : POSITION;
		half4 color : COLOR;
		half2 uv : TEXCOORD0;
	};
	
	uniform sampler2D _MainTex; 
	
	uniform half _outLineSize;
	uniform fixed4 _outLineColor;
	
	uniform fixed4 _ChangeColor;
	uniform half _Blend;
	ENDCG
 
	SubShader {
		Tags { "Queue" = "Overlay" }
		LOD 100
 
		Pass {
            Name "Outline"
            Cull Front
//            ZWrite Off
//			ZTest Always
			
//			Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            v2f vert (appdata v) {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, half4(v.vertex.xyz + v.normal*_outLineSize,1));
                return o;
            }
            
            fixed4 frag(v2f i) : COLOR {
                return fixed4(_outLineColor.rgb, 1);
            }
            ENDCG
        }
 
		Pass {
			Name "BASE"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}
			
			fixed4 frag(v2f i) : COLOR {
				return lerp(tex2D(_MainTex, i.uv), _ChangeColor, _Blend);
			}
			ENDCG
		}
	}
    FallBack "Unlit/Texture"
    CustomEditor "ShaderForgeMaterialInspector"
}