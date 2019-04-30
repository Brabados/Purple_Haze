Shader "Custom/Dissolve" {

	Properties {

		_Color ("Color Tint", Color) = (1,1,1,1)
		_DissolveColor ("Dissolve Color", Color) = (1,1,1,1)
		_EmissionMult ("Emission Multiplier", Range(0,10)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseMap ("Noise Map", 2D) = "white" {}
		_DissolveThreshold ("Dissolve Threshold", Range(0,1.2)) = 0
		_ColorThreshold ("Color Threshold", Range(0,1)) = 0.3

	}
	
	SubShader {

		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		
		// Add shadow forces unity to recalculate the shadow pass according to the surf code
		#pragma surface surf Standard addshadow
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _NoiseMap;
		float _DissolveThreshold;
		float _EmissionMult;
		float _ColorThreshold;
		fixed4 _Color;
		fixed4 _DissolveColor;
		
		half _Glossiness;
        half _Metallic;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 n = tex2D(_NoiseMap, IN.uv_MainTex);

			o.Albedo = lerp(c.rgb, _DissolveColor, step(n.r - (_ColorThreshold * _DissolveThreshold) + (0.1 - _DissolveThreshold), _DissolveThreshold));
			o.Alpha = c.a;
			o.Emission = lerp(0, _DissolveColor * _EmissionMult, step(n.r - (_ColorThreshold * _DissolveThreshold) + (0.1 - (0.4 * _DissolveThreshold)), _DissolveThreshold));
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

			clip(lerp(-1, 1, step(_DissolveThreshold, n.r)));

		}
		ENDCG
	}
	FallBack "Diffuse"
}
