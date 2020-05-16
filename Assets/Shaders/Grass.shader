//Tutorial from:
//https://roystan.net/articles/grass-shader.html

Shader "Custom/JoeGrass"
{
    Properties
    {
		[Header(Shading)]
        _TopColor("Top Color", Color) = (1,1,1,1)
		_BottomColor("Bottom Color", Color) = (1,1,1,1)
		_TranslucentGain("Translucent Gain", Range(0,1)) = 0.5
		_BendRotationRandom("Bend Rotation Random", Range(0,1)) = 0.2
		_BladeWidth("Blade Width", Float) = 0.05
		_BladeWidthRandom("Blade Width Random", Float) = 0.02
		_BladeHeight("Blade Height", Float) = 0.5
		_BladeHeightRandom("Blade Height Random", Float) = 0.3
		_BladeDepth("Blade Depth", Float) = 0.05
		_BladeDepthRandom("Blade Depth Random", Float) = 0.02
		_TessellationUniform("Tessellation Uniform", Range(1,64)) = 1
		_WindDistortionMap("Wind Distortion Map", 2D) = "white" {}
		_WindFrequency("Wind Frequency", Vector) = (0.05, 0.05, 0, 0)
		_WindStrength("Wind Strength", Float) = 1
		_GrassCutoff("Grass Cutoff", Range(-1,1)) = 0.5
		_MinHeightCutoff("Min Height Cutoff", Range(0,5)) = 0.5
		_MaxHeightCutoff("Max Height Cutoff", Range(0,5)) = 0.5
    }

	CGINCLUDE
	#include "UnityCG.cginc"
	#include "Autolight.cginc"
	#include "CustomTessellation.cginc"

	#define BLADE_SEGMENTS 3

	float _BendRotationRandom;

	float _BladeHeight;
	float _BladeHeightRandom;
	float _BladeWidth;
	float _BladeWidthRandom;
	float _BladeDepth;
	float _BladeDepthRandom;

	sampler2D _WindDistortionMap;
	float4 _WindDistortionMap_ST;
	float2 _WindFrequency;
	float _WindStrength;

	float _GrassCutoff;
	float _MinHeightCutoff;
	float _MaxHeightCutoff;

	struct geometryOutput
	{
		float2 uv : TEXCOORD0;
		float4 pos : SV_POSITION;
		unityShadowCoord4 _ShadowCoord : TEXCOORD1;
		float3 normal : NORMAL;
	};

	float rand(float3 co)
	{
		return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
	}

	float3x3 AngleAxis3x3(float angle, float3 axis)
	{
		float c, s;
		sincos(angle, s, c);

		float t = 1 - c;
		float x = axis.x;
		float y = axis.y;
		float z = axis.z;

		return float3x3(
			t * x * x + c, t * x * y - s * z, t * x * z + s * y,
			t * x * y + s * z, t * y * y + c, t * y * z - s * x,
			t * x * z - s * y, t * y * z + s * x, t * z * z + c
		);
	}

	geometryOutput VertexOutput(float3 pos, float2 uv, float3 normal)
	{
		geometryOutput o;
		o.pos = UnityObjectToClipPos(pos);
		o.uv = uv;
		o.normal = UnityObjectToWorldNormal(normal);
		o._ShadowCoord = ComputeScreenPos(o.pos);

		#if UNITY_PASS_SHADOWCASTER
			o.pos = UnityApplyLinearShadowBias(o.pos);
		#endif

		return o;
	}

	geometryOutput GenerateGrassVertex(float3 vertexPosition, float width, float depth, float height, float2 uv, float3x3 transformMatrix){
		float3 tangentPoint = float3(width, depth, height);

		float3 tangentNormal = float3(0, -1, 0);
		float3 localNormal = mul(transformMatrix, tangentNormal);

		float3 localPosition = vertexPosition + mul(transformMatrix, tangentPoint);
		return VertexOutput(localPosition, uv, localNormal);
	}

	[maxvertexcount(BLADE_SEGMENTS * 10)]
	void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> triStream){
		float3 pos = IN[0].vertex;
		float3 origin = float3(0,0,0);

		float3 vNormal = IN[0].normal;
		if(dot(vNormal, normalize(pos.xyz)) > _GrassCutoff && distance(pos, origin) > _MinHeightCutoff && distance(pos, origin) < _MaxHeightCutoff){
			float4 vTangent = IN[0].tangent;
			float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;

			float3x3 tangentToLocal = float3x3(
				vTangent.x, vBinormal.x, vNormal.x,
				vTangent.y, vBinormal.y, vNormal.y,
				vTangent.z, vBinormal.z, vNormal.z
			);

			float3x3 facingRotationMatrix = AngleAxis3x3(rand(pos) * UNITY_TWO_PI, float3(0, 0, 1));
			float3x3 bendRotationMatrix = AngleAxis3x3(rand(pos.zzx) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));

			float2 uv = pos.xz * _WindDistortionMap_ST.xy + _WindDistortionMap_ST.zw + _WindFrequency * _Time.y;
			float2 windSample = (tex2Dlod(_WindDistortionMap, float4(uv, 0, 0)).xy * 2 - 1) * _WindStrength;
			float3 wind = normalize(float3(windSample.x, windSample.y, 0));
			float3x3 windRotation = AngleAxis3x3(UNITY_PI * windSample, wind);

			float3x3 transformationMatrix = mul(mul(mul(tangentToLocal, windRotation), facingRotationMatrix), bendRotationMatrix);
			float3x3 transformationMatrixFacing = mul(tangentToLocal, facingRotationMatrix);

			float height = (rand(pos.zyx) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
			float width = (rand(pos.xzy) * 2 - 1) * _BladeWidthRandom + _BladeWidth;
			float depth = (rand(pos.yxz) * 2 - 1) * _BladeDepthRandom + _BladeDepth;

			//FRONT
			triStream.Append(GenerateGrassVertex(pos, 0, 0, 0, float2(0, 0), transformationMatrixFacing));					//A
			triStream.Append(GenerateGrassVertex(pos, width, 0, 0, float2(1, 0), transformationMatrixFacing));				//B
			triStream.Append(GenerateGrassVertex(pos, 0, 0, height, float2(0.5, 1), transformationMatrix));					//C

			triStream.Append(GenerateGrassVertex(pos, width, 0, 0, float2(1, 0), transformationMatrixFacing));				//B
			triStream.Append(GenerateGrassVertex(pos, 0, 0, height, float2(0.5, 1), transformationMatrix));					//C
			triStream.Append(GenerateGrassVertex(pos, width, 0, height, float2(0.5, 1), transformationMatrix));				//D

			//TOP		
			triStream.Append(GenerateGrassVertex(pos, 0, 0, height, float2(0.5, 1), transformationMatrix));					//C
			triStream.Append(GenerateGrassVertex(pos, width, 0, height, float2(0.5, 1), transformationMatrix));				//D
			triStream.Append(GenerateGrassVertex(pos, 0, depth, height, float2(0.5, 1), transformationMatrix));				//G

			triStream.Append(GenerateGrassVertex(pos, width, 0, height, float2(0.5, 1), transformationMatrix));				//D
			triStream.Append(GenerateGrassVertex(pos, 0, depth, height, float2(0.5, 1), transformationMatrix));				//G
			triStream.Append(GenerateGrassVertex(pos, width, depth, height, float2(0.5, 1), transformationMatrix));			//H

			//BACK		
			triStream.Append(GenerateGrassVertex(pos, 0, depth, height, float2(0.5, 1), transformationMatrix));				//G
			triStream.Append(GenerateGrassVertex(pos, width, depth, height, float2(0.5, 1), transformationMatrix));			//H
			triStream.Append(GenerateGrassVertex(pos, 0, depth, 0, float2(0.5, 1), transformationMatrixFacing));			//E

			triStream.Append(GenerateGrassVertex(pos, width, depth, height, float2(0.5, 1), transformationMatrix));			//H
			triStream.Append(GenerateGrassVertex(pos, 0, depth, 0, float2(0.5, 1), transformationMatrixFacing));			//E
			triStream.Append(GenerateGrassVertex(pos, width, depth, 0, float2(0.5, 1), transformationMatrixFacing));		//F

			//RIGHT		
			triStream.Append(GenerateGrassVertex(pos, width, depth, height, float2(0.5, 1), transformationMatrix));			//H
			triStream.Append(GenerateGrassVertex(pos, width, depth, 0, float2(0.5, 1), transformationMatrixFacing));		//F
			triStream.Append(GenerateGrassVertex(pos, width, 0, 0, float2(1, 0), transformationMatrixFacing));				//B

			triStream.Append(GenerateGrassVertex(pos, width, depth, height, float2(0.5, 1), transformationMatrix));			//H
			triStream.Append(GenerateGrassVertex(pos, width, 0, 0, float2(1, 0), transformationMatrixFacing));				//B
			triStream.Append(GenerateGrassVertex(pos, width, 0, height, float2(0.5, 1), transformationMatrix));				//D

			//LEFT		
			triStream.Append(GenerateGrassVertex(pos, 0, depth, height, float2(0.5, 1), transformationMatrix));				//G
			triStream.Append(GenerateGrassVertex(pos, 0, 0, height, float2(0.5, 1), transformationMatrix));					//C
			triStream.Append(GenerateGrassVertex(pos, 0, depth, 0, float2(0.5, 1), transformationMatrixFacing));			//E

			triStream.Append(GenerateGrassVertex(pos, 0, 0, height, float2(0.5, 1), transformationMatrix));					//C
			triStream.Append(GenerateGrassVertex(pos, 0, depth, 0, float2(0.5, 1), transformationMatrixFacing));			//E
			triStream.Append(GenerateGrassVertex(pos, 0, 0, 0, float2(0, 0), transformationMatrixFacing));					//A
		}
	}
	ENDCG

    SubShader
    {
		Cull Off

        Pass
        {
			Tags
			{
				"RenderType" = "Opaque"
				"LightMode" = "ForwardBase"
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma geometry geo
			#pragma target 4.6
			#pragma multi_compile_fwdbase
			#pragma hull hull
			#pragma domain domain
            
			#include "Lighting.cginc"

			float4 _TopColor;
			float4 _BottomColor;
			float _TranslucentGain;

			float4 frag (geometryOutput i, fixed facing : VFACE) : SV_Target
            {	
				float3 normal = facing > 0 ? i.normal : - i.normal;

				float shadow = SHADOW_ATTENUATION(i);
				float NdotL = saturate(saturate(dot(normal, _WorldSpaceLightPos0)) + _TranslucentGain) * shadow;

				float3 ambient = ShadeSH9(float4(normal, 1));
				float4 lightIntensity = NdotL * _LightColor0 + float4(ambient, 1);
				float4 col = lerp(_BottomColor, _TopColor * lightIntensity, i.uv.y);

				return col;
            }
           
		    ENDCG
        }

		Pass
		{
			Tags 
			{
				"LightMode" = "ShadowCaster"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geo
			#pragma fragment frag
			#pragma hull hull
			#pragma domain domain
			#pragma target 4.6
			#pragma multi_compile_shadowcaster

			float4 frag(geometryOutput i) : SV_Target
			{
				SHADOW_CASTER_FRAGMENT(i)
			}

			ENDCG
		}
    }
}