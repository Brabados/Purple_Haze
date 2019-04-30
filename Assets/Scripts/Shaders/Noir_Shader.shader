Shader "Hidden/Noir_Shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				float4 colorin, colorR, colorB;
				colorin = tex2D(_MainTex, i.uv);


				colorin = pow(colorin, 0.45f);

				float gray = dot(colorin.rgb, float3(0.333f,0.333f,0.333f));


				float weightR = smoothstep(0.1f, 0.3f, colorin.r - gray);
				float weightB = smoothstep(0.1f, 0.3f, colorin.b - gray);


				gray = (gray - 0.5f - 0.2) * 3 + 0.5;

				colorR.rgb = lerp(gray, colorin.rgb * float3(1.1f, 0.5f, 0.5f), weightR);
				colorB.rgb = lerp(gray, colorin.rgb * float3(0.5f, 0.5f, 1.1f), weightB);

				colorin.r = colorR.r;
				colorin.b = colorB.b;
				colorin.g = colorB.g;

				colorin.rgb = pow(colorin.rgb,2.2f);

				return colorin;

			}
			ENDCG
		}
	}
}
