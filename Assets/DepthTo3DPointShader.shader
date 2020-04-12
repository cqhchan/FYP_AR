
Shader "Custom/DepthTo3DPointShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Camera_cx("cx", float) = 0.0
		_Camera_cy("cy", float) = 0.0
		_Camera_fx("fx", float) = 0.0
		_Camera_fy("fy", float) = 0.0
		_Camera_ResolutionX("ResoX", int) = 0
		_Camera_ResolutionY("ResoY", int) = 0
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
			uniform float _Camera_cx;
			uniform float _Camera_cy;
			uniform float _Camera_fx;
			uniform float _Camera_fy;
			uniform int _Camera_ResolutionX;
			uniform int _Camera_ResolutionY;

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

				o.uv.x = 1.0 - o.uv.x;
				o.uv.y = 1.0 - o.uv.y;


				return o;
			}

			fixed4 frag(output o) : COLOR
			{


				float depth = (tex2D(_MainTex, o.uv)).x;
				
				int uInPixel = o.uv.x * _Camera_ResolutionX;
				int vInPixel = (1 - o.uv.y) * _Camera_ResolutionY;
				
			    float z = (depth);
                float x = ((uInPixel - _Camera_cx) / _Camera_fx) * z;
                float y = ((vInPixel - _Camera_cy) / _Camera_fy) * z;

				
				return float4(x,y,z,0);
				

			}

			ENDCG
		}
	}
}