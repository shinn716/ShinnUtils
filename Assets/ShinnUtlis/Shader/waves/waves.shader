Shader "Custom/waves" {


	Properties
	{
		_CenterX("CenterX", float) = 110
		_CenterY("CenterY", float) = 139
		_CenterZ("CenterZ", float) = 0

		_ColorR("Red", Range(0,255)) = 0
		_ColorG("Green", Range(0,255)) = 0
		_ColorB("Blue", Range(0,255)) = 0

		_CircleColor("CircleColor", Color) = (1, 1, 1, 1)

		_Thickness("Thickness", Range(0, 5))=3
		_Speed("Speed", Range(0, 5)) = 1
		//_Alpha("Alpha", Range(0,1)) = 1
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard 
#pragma target 3.0

		struct Input {
		float3 worldPos;
		};

	float _CenterX;
	float _CenterY;
	float _CenterZ;
	float _ColorR;
	float _ColorG;
	float _ColorB;
	float _Speed;
	float _Thickness;

	float4 _CircleColor;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		float dist = distance(fixed3(_CenterX, _CenterY, _CenterZ), IN.worldPos);
		float val = abs(sin(dist*_Thickness - _Time * 100 * _Speed));

		if (val > 0.98) {
			o.Albedo = fixed4(_CircleColor.r, _CircleColor.g, _CircleColor.b, _CircleColor.a);
		}
		else {
			o.Albedo = fixed4(_ColorR / 255.0, _ColorG / 255.0, _ColorB / 255.0, 1);
		}
	}
	ENDCG
	}
		FallBack "Diffuse"
}