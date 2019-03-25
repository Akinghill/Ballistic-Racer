// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'



Shader "Unlit/toonShaderTest"
{
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		//_Outline("Outline width", Range(.002, 0.03)) = .005
		_Outline("Outline width", Range(0.1, 0.3)) = .3
		_MainTex("Base (RGB)", 2D) = "white" { }
	    _BumpMap("Normal Map", 2D) = "bump" {}
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v) {
		
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

	SubShader{
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _BumpMap_ST;
	
		fixed4 _Color;

		struct Input {
		float2 uv_MainTex;
		
	};
		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			half3 tspace0 : TEXCOORD1; // tangent.x, bitangent.x, normal.x
			half3 tspace1 : TEXCOORD2; // tangent.y, bitangent.y, normal.y
			half3 tspace2 : TEXCOORD3; // tangent.z, bitangent.z, normal.z
									   /*half diffuse : TEXCOORD1;
									   half night : TEXCOORD2;*/
			half3 viewDir : TEXCOORD4;
			half3 normalDir : TEXCOORD5;
		};
		vertexOutput vert(appdata_tan input)
		{
			vertexOutput output;
			output.pos = UnityObjectToClipPos(input.vertex);
			output.uv = input.texcoord;

			output.viewDir = normalize(ObjSpaceViewDir(input.vertex));
			output.normalDir = input.normal;

			half3 wNormal = UnityObjectToWorldNormal(input.normal);
			half3 wTangent = UnityObjectToWorldDir(input.tangent.xyz);
			// compute bitangent from cross product of normal and tangent
			half tangentSign = input.tangent.w * unity_WorldTransformParams.w;
			half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
			// output the tangent space matrix
			output.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
			output.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
			output.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);

			return output;
		}
		half4 frag(vertexOutput input) : SV_Target
		{
			//half2 uv_MainTex = TRANSFORM_TEX(input.uv, _MainTex);
			half2 uv_BumpMap = TRANSFORM_TEX(input.uv, _BumpMap);


			half3 tnormal = UnpackNormal(tex2D(_BumpMap, uv_BumpMap));
			// transform normal from tangent to world space
			half3 worldNormal;
			worldNormal.x = dot(input.tspace0, tnormal);
			worldNormal.y = dot(input.tspace1, tnormal);
			worldNormal.z = dot(input.tspace2, tnormal);
		}

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

	
		Pass{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front
		ZWrite On
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha


		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		half4 frag(v2f i) :COLOR{ return i.color; }
		ENDCG
		}


	}

	SubShader{
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;

		
		
		
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

		Pass{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front
		ZWrite On
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha

		
		SetTexture[_MainTex]{ combine primary }
	}
	}

		Fallback "Diffuse"
}
