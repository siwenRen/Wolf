Shader "Lu/OutlineTest" 
{
	Properties 
	{
		// outline
		_OutlineColor ("Outline Color", Color) = (1,1,1,0.5)
		_OutlineWidth ("Outline Width", Range(0.0,0.1)) = 0.02
		
		// combine
		_Num ("Combine Number", Range(0, 8)) = 0
		_CombineTex1 ("Combine Tex1", 2D) = "white" {}
		_CombineTex2 ("Combine Tex2", 2D) = "white" {}
		_CombineTex3 ("Combine Tex3", 2D) = "white" {}
		_CombineTex4 ("Combine Tex4", 2D) = "white" {}
		_CombineTex5 ("Combine Tex5", 2D) = "white" {}
		_CombineTex6 ("Combine Tex6", 2D) = "white" {}
		_CombineTex7 ("Combine Tex7", 2D) = "white" {}
//		_CombineTex8 ("Combine Tex8", 2D) = "white" {}
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uvs[4] : TEXCOORD1;
		fixed4 color : COLOR;
	};
	
	// outline
	uniform float4 _OutlineColor;
	uniform float  _OutlineWidth;
	
	// combine
	// the max number of texture is 8
	uniform int _Num;
	
	uniform sampler2D _CombineTex1;
	uniform sampler2D _CombineTex2;
	uniform sampler2D _CombineTex3;
	uniform sampler2D _CombineTex4;
	uniform sampler2D _CombineTex5;
	uniform sampler2D _CombineTex6;
	uniform sampler2D _CombineTex7;
//	uniform sampler2D _CombineTex8;

	uniform float4 _MainTex_TexelSize;
	uniform sampler2D _CameraDepthTexture;
	uniform sampler2D _CameraNormalsTexture;

	ENDCG
	
	
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200
		
		// outline
		Pass 
		{
			Tags { "Queue" = "Transparent"} 
			
//			Cull Front
//			ZWrite Off
//			ZTest Always
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			v2f vert(appdata_full v) 
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 dir = TransformViewToProjection(norm.xy);

//				o.pos.xy += dir * _OutlineWidth;
				o.color = v.color;
				o.uv = v.texcoord.xy;
				
				float2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
				o.uvs[0] = uv;
				#if SHADER_API_D3D9
				if (_MainTex_TexelSize.y < 0)
					uv.y = 1 - uv.y;
				#endif
				o.uvs[1] = uv;
				o.uvs[2] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y);
				o.uvs[3] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y);
				
				return o;
			}
			
			float4 frag(v2f i) : COLOR {
//				half4 original = tex2D(_MainTex, i.uvs[0]);
				float centerDepth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uvs[1]));
				float3 centerNormal = tex2D(_CameraNormalsTexture, i.uvs[1]) * 2.0 - 1.0;
				
				float d1 = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uvs[2]));
				float3 n1 = tex2D(_CameraNormalsTexture, i.uvs[2]) * 2.0 - 1.0;
				
				float d2 = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uvs[2]));
				float3 n2 = tex2D(_CameraNormalsTexture, i.uvs[2]) * 2.0 - 1.0;
				
				half isD1 = abs(d1 - centerDepth) < 0.01 * centerDepth;
				half isD2 = abs(d2 - centerDepth) < 0.01 * centerDepth;
				half isN1 = 1 - dot(n1, centerNormal) < 0.05;
				half isN2 = 1 - dot(n2, centerNormal) < 0.05;
				
				float4 color;
				if (_Num == 0) 
				{
					color = float4(0, 0, 0, 1);
				} 
				else 
				{
					float perAlpha = 1.0 / _Num; 
					do {
						if (i.color.a <= perAlpha * 1)
						{
							color = tex2D(_CombineTex1, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 2)
						{
							color = tex2D(_CombineTex2, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 3)
						{
							color = tex2D(_CombineTex3, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 4)
						{
							color = tex2D(_CombineTex4, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 5)
						{
							color = tex2D(_CombineTex5, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 6)
						{
							color = tex2D(_CombineTex6, i.uv);
							break;
						}
						if (i.color.a <= perAlpha * 7)
						{
							color = tex2D(_CombineTex7, i.uv);
							break;
						}
//						if (i.color.a <= perAlpha * 8)
//						{
//							color = tex2D(_CombineTex8, i.uv);
//							break;
//						}
					} while(false);
				}
				
				return color * isD1 * isD2 ;
			}
			
			ENDCG
		}
		
		// combine
//		Pass 
//		{
//			Tags { "Queue" = "Transparent"} 
//			
//			CGPROGRAM
//			
//			// gl graphics card 3.0 max texture number is more than 7
//			// 2.0 is 7
////			#pragma target 3.0
//			
//			#pragma vertex vert
//			#pragma fragment frag
//			
//			v2f vert(appdata_full v) 
//			{
//				v2f o;
//				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.uv = v.texcoord.xy;
//				o.color = v.color;
//				return o;
//			}
//			
//			float4 frag(v2f i) : COLOR {
//				float4 color;
//				if (_Num == 0) 
//				{
//					color = float4(0, 0, 0, 1);
//				} 
//				else 
//				{
//					float perAlpha = 1.0 / _Num; 
//					do {
//						if (i.color.a <= perAlpha * 1)
//						{
//							color = tex2D(_CombineTex1, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 2)
//						{
//							color = tex2D(_CombineTex2, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 3)
//						{
//							color = tex2D(_CombineTex3, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 4)
//						{
//							color = tex2D(_CombineTex4, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 5)
//						{
//							color = tex2D(_CombineTex5, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 6)
//						{
//							color = tex2D(_CombineTex6, i.uv);
//							break;
//						}
//						if (i.color.a <= perAlpha * 7)
//						{
//							color = tex2D(_CombineTex7, i.uv);
//							break;
//						}
////						if (i.color.a <= perAlpha * 8)
////						{
////							color = tex2D(_CombineTex8, i.uv);
////							break;
////						}
//					} while(false);
//				}
//				return color;
//			}
//			
//			ENDCG
//		}
	} 
	FallBack "Diffuse"
}
