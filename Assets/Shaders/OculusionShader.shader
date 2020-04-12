// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OculusionShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_ExternalCameraTexture("Base (RGB)", 2D) = "white" {}
		_DepthTestTexture("Base (RGB)", 2D) = "white" {}

	}
		SubShader
		{
			Pass
			{
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform sampler2D _ExternalCameraTexture;
				uniform sampler2D _CameraDepthTexture;
				uniform sampler2D _DepthTestTexture;
				uniform fixed _DepthLevel;
				uniform half4 _MainTex_TexelSize;

				struct input
				{
					float4 pos : POSITION;
					half2 uv : TEXCOORD0;
				};

				struct output
				{
					float4 pos : SV_POSITION;
					half2 uv : TEXCOORD0;
				};


				output vert(input i)
				{
					output o;
					o.pos = UnityObjectToClipPos(i.pos);
					o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, i.uv);
					// why do we need this? cause sometimes the image I get is flipped. see: http://docs.unity3d.com/Manual/SL-PlatformDifferences.html
					#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
							o.uv.y = 1 - o.uv.y;
					#endif

					return o;
				}

				fixed4 frag(output o) : COLOR
				{
					float depth = (Linear01Depth(tex2D(_CameraDepthTexture, o.uv))) ;
					float2 tempUV = float2(o.uv.x, 1 - o.uv.y);
					float depthTest = (tex2D(_DepthTestTexture, tempUV)) / 20;
					if (depth < depthTest) {

						fixed4 col = tex2D(_MainTex, o.uv);

						if (col.w == 0) {
							fixed4 col = tex2D(_ExternalCameraTexture, tempUV);
							float r = col.z;
							float b = col.x;
							col.x = r;
							col.z = b;
							return col;
						}

						return col;
					}
					else {
						fixed4 col = tex2D(_ExternalCameraTexture, tempUV);
						float r = col.z;
						float b = col.x;
						col.x = r;
						col.z = b;
						return col;
					}

				}

				ENDCG
			}
		}
}