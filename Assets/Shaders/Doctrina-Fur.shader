Shader "Doctrina/Fur" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_WindAmplitude ("Wind Amplitude", Float) = 0.01
	_WindFrequency ("Wind Frequency", Float) = 5
	_WindDistribution ("Wind Distribution", Float) = 120
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 200
	
CGPROGRAM
#pragma surface surf Fur vertex:vert addshadow alphatest:_Cutoff

sampler2D _MainTex;
fixed4 _Color;
half _WindAmplitude;
half _WindFrequency;
half _WindDistribution;

struct Input {
	float2 uv_MainTex;
};

 void vert (inout appdata_full v, out Input o) {
      v.vertex.x += abs( sin(v.vertex.z * _WindDistribution + _Time.y * _WindFrequency) * _WindAmplitude) * v.texcoord.y;
      v.vertex.z += abs( sin(v.vertex.y * _WindDistribution + _Time.y * _WindFrequency) * _WindAmplitude) * v.texcoord.y;

      UNITY_INITIALIZE_OUTPUT(Input,o);
 }

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}

half4 LightingFur (SurfaceOutput s, half3 lightDir, half atten) {
     half NdotL = dot (s.Normal, lightDir);
     half diff = NdotL * 0.5 + 0.5;
     half4 c;
     c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten);
     c.a = s.Alpha;
     return c;
 }


ENDCG
}

Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
