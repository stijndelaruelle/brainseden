Shader "Custom/BaseShader" {
	Properties 
	{
		_ColorMap ("Color (RGB)", 2D) = "white" {}
		_MasksMap ("Alpha (R), AO (G), not used (B)",2D) = "white" {}
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_ColorMap;
			float2 uv2_MasksMap;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_ColorMap, IN.uv_ColorMap);
			half4 masks = tex2D (_MasksMap, IN.uv2_MasksMap);
			
			half3 finalColor = c.rgb * masks.g;
			
			o.Albedo = finalColor;
			o.Alpha = masks.r;			
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
