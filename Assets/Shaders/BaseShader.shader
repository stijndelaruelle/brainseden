Shader "Custom/BaseShader" {
	Properties 
	{
		_ColorTex ("Color (RGB)", 2D) = "white" {}
		_VariationTex("Variations (RGB)", 2D) = "white" {}
		_AOStrength ("AO Strength", Range(0,1)) = 0.5
		_LightContrast("Light Contrast", float) = 5
		_LightSteps ("Light Steps", float) = 10
		_MinLightValue("Min Light Value", Range(0,1)) = 0.3
		_WorldVariationTiling ("World Variation Tiling", float) = 10
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Custom

		sampler2D _ColorTex;
		sampler2D _VariationTex;
		float _AOStrength;
		float _LightContrast;
		float _LightSteps;
		float _MinLightValue;
		float _WorldVariationTiling;
		
		struct Input 
		{
			float2 uv_ColorTex;
			float3 worldPos;
		};

  		half4 LightingCustom (SurfaceOutput s, half3 lightDir, half atten)
  		{
	        half NdotL = (dot (s.Normal, lightDir)*0.5f)+0.5f;
	        NdotL = saturate(pow(NdotL,_LightContrast));
	        //bring in range
	        NdotL = (NdotL * (1.0f-_MinLightValue))+_MinLightValue;
	        //flatten
	        NdotL = floor(NdotL*_LightSteps)/_LightSteps;

			half4 c;
	        c.rgb = s.Albedo * _LightColor0.rgb * NdotL;
	        c.a = s.Alpha;
	        
       		return c;
    	}
    
		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_ColorTex, IN.uv_ColorTex);
			
			half mapOne   = tex2D (_VariationTex, float2(IN.worldPos.x,IN.worldPos.y)/_WorldVariationTiling).r;
			half mapTwo   = tex2D (_VariationTex, float2(IN.worldPos.y,IN.worldPos.z)/_WorldVariationTiling).g;
			half mapThree = tex2D (_VariationTex, float2(IN.worldPos.x,IN.worldPos.z)/_WorldVariationTiling).b;
			
			half combined = mapOne + mapTwo + mapThree;
			combined = combined/3.0f;

			half3 finalColor = c.rgb*combined;
						
			o.Albedo = finalColor;	
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
