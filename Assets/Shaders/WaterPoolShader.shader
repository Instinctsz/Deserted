Shader "Unlit/Toon Water Shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
		_DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
		_DepthMaxDistance("Depth Maximum Distance", Float) = 1
		_SurfaceNoise("Surface Noise", 2D) = "white" {}
		_SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
		_FoamDistance("Foam Distance", Float) = 0.4
		_SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)

		_WaveStrength("Wave Strength", Range(0, 1)) = 50
		_WaveSpeed("Wave Speed", Range(0, 200)) = 100
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

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
					float4 screenPosition : TEXCOORD2;
					float2 noiseUV : TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				float4 _DepthGradientShallow;
				float4 _DepthGradientDeep;

				float _DepthMaxDistance;

				sampler2D _CameraDepthTexture;

				sampler2D _SurfaceNoise;
				float4 _SurfaceNoise_ST;

				float _WaveStrength;
				float _WaveSpeed;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);

					o.screenPosition = ComputeScreenPos(o.vertex);
					o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);

					float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
					float displacement = (sin(worldPos.x + (_WaveSpeed * _Time)));
					worldPos.y = worldPos.y + (displacement * _WaveStrength);
					o.vertex = mul(UNITY_MATRIX_VP, worldPos);


					return o;
				}

				float _SurfaceNoiseCutoff;
				float _FoamDistance;
				float2 _SurfaceNoiseScroll;

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);


				float waterDepth = tex2Dproj(_CameraDepthTexture, i.screenPosition).r;
				float waterDepthLinear = LinearEyeDepth(waterDepth);
				float depthDifference = waterDepthLinear - i.screenPosition.w;


				float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
				float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

				float2 noiseUV = float2(i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x, i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y);
				float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;

				// Foam
				float foamDepthDifference = saturate(depthDifference / _FoamDistance);
				float surfaceNoiseCutoff = foamDepthDifference * _SurfaceNoiseCutoff;
				float surfaceNoise = surfaceNoiseSample > surfaceNoiseCutoff ? 1 : 0;

				//return waterColor + ceil(1 - foamDepthDifference);
				return depthDifference;
			}
			ENDCG
		}
		}
}
