Shader "Custom/DiffusePBR" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,0)
		_MainTex ("MainTex", 2D) = "white" {}
		_SkinMask ("SkinMask", 2D) = "white" {}
		_SkinTexture ("SkinTexture", 2D) = "white" {}
		_NormalMap ("NormalMap", 2D) = "bump" {}
		_RMA ("RMA", 2D) = "white" {}
		_Rougness ("Rougness", Range(0, 10)) = 0
		_Matalic ("Matalic", Range(0, 1)) = 0
		_NormalScale ("NormalScale", Range(0, 1.5)) = 0
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}