
Shader "Custom/SceneShader" 
{
	Properties
	{
		_MainTex("MainTexture", 2D)	  = "white" {}
		_DepthTex("DepthTexture", 2D) = "white" {}

			 _WORLDPOS_X_OFFS("WORLDPOS_X_OFFS", float) = 10
			 _WORLDPOS_Y_OFFS("WORLDPOS_Y_OFFS", float) =  0
			 _WORLDPOS_X_SIZE("WORLDPOS_X_SIZE", float) = 20
			 _WORLDPOS_Y_SIZE("WORLDPOS_Y_SIZE", float) = 180
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Tags {"LightMode" = "ForwardBase"}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma only_renderers d3d11

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
		
			//static const float3 FIXED_CAM_POS = float3(0, 11.54701, 0);
			static const float3 FIXED_CAM_POS = float3(0, 11.54701, 0);

			static const float SCALEFACTOR = 100;
			static const float TAN30DEG = 0.57735026918962576450914878050196;
			static const float WORLDPOS_X_OFFS = 45;//10 1/2 от WORLDPOS_Y_OFFS
			static const float WORLDPOS_Y_OFFS =  90;//0 равно WORLDPOS_X_SIZE
			static const float WORLDPOS_X_SIZE = 90;//10 масштаб текстуры задника по X
			static const float WORLDPOS_Y_SIZE = 180;//20 масштаб текстуры задника по Z

			struct appdata
			{
				float4 vertex	: POSITION;
				float3 normal   : NORMAL;
				float2 uv		: TEXCOORD0;
				fixed3 color	: COLOR0;
			};

			struct v2f
			{
				float4 vertex		: SV_POSITION;
				float2 uv			: TEXCOORD0;
				float3 worldPos		: TEXCOORD1;
				float4 diffuse		: COLOR0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			sampler2D_float _DepthTex;
			float4 _DepthTex_ST;
				
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				// Simple lighting
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.diffuse = nl * _LightColor0;

				return o;
			}
			
			float4 frag(v2f i) : SV_Target
			{	
				i.uv = float2(1,1) - i.uv;
				float4 colObj = tex2D(_MainTex, i.uv) * i.diffuse;

				float2 DepthUV = (i.worldPos.xz + float2(WORLDPOS_X_OFFS, WORLDPOS_Y_OFFS));
				DepthUV /= float2(WORLDPOS_X_SIZE, WORLDPOS_Y_SIZE);
				DepthUV.y += (i.worldPos.y / TAN30DEG / WORLDPOS_Y_SIZE);
				
				float DistOld = DecodeFloatRGBA(tex2D(_DepthTex, DepthUV)) * SCALEFACTOR;

				float3 vDistNew = i.worldPos - FIXED_CAM_POS;
				float DistNew = length(float3(0, vDistNew.y, vDistNew.z));
				
				if (DistNew > DistOld) discard;
				return(colObj);
				
			}

			ENDCG
		}
	}
}
