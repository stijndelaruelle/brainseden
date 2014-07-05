Shader "Custom/BaseShader" {
	Properties 
	{
		_ColorTex ("Color (RGB)", 2D) = "white" {}
		_MasksTex ("Alpha (R), AO (G), not used (B)",2D) = "white" {}
		_AOStrength ("AO Strength", Range(0,1)) = 0.5
		_LightContrast("Light Contrast", float) = 5
		_LightSteps ("Light Steps", float) = 10
		_MinLightValue("Min Light Value", Range(0,1)) = 0.3
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Custom

		sampler2D _ColorTex;
		sampler2D _MasksTex;
		float _AOStrength;
		float _LightContrast;
		float _LightSteps;
		float _MinLightValue;
		
		struct Input 
		{
			float2 uv2_ColorTex;
			float2 uv_MasksTex;
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
			half4 c = tex2D (_ColorTex, IN.uv2_ColorTex);
			half4 masks = tex2D (_MasksTex, IN.uv_MasksTex);
			
			half ao = saturate(masks.g + (1.0f-_AOStrength));
			half3 finalColor = c.rgb * ao;
						
			o.Albedo = finalColor;
			o.Alpha = masks.r;			
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
