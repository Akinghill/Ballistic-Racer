Shader "Unlit/shader1"
{
	Properties
	{
		MainTex ("Texture", 2D) = "white" {}
		tintColor("tint color", Color) = (1,1,1,1)
		transparency("transparency", Range(0.0, 0.75)) = 0.25
		CutOut("CutOut", Range(0.0, 1.0)) = 0.2
		Distance("Distance", float) = 1
		Amplitude("Amplitude", float) = 1
		Speed("Speed", float) = 1
		Amount("Amounty", Range(0.0, 1.0)) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100
		ZWrite off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 tintColor;
			float transparency;
			float CutOut;
			float Amount;
			float Speed;
			float Amplitude;
			float Distance;

			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.x += sin(_Time.y * Speed + v.vertex.y * Amplitude) * Distance * Amount; //Vertexes and all values a multiplied
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) + tintColor;
				col.a = transparency;
				clip(col.r - CutOut);
				
				return col;
			}
			ENDCG
		}
	}
}
