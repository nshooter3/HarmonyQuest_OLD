// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Sprites/Sprites-SlashInHalf"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_MultColor("MultColor", Color) = (1,1,1,1)
		_MultStrength("MultStrength", Range(0.000000,1.000000)) = 0.000000
		_Slope("Slope", Range(-3.000000,3.000000)) = 0.000000
		_IsSectionA("IsSectionA", Range(0,1)) = 0
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
				float3 localPos : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			fixed4 _Color;
			fixed4 _MultColor;
			float _MultStrength;
			float _Slope;
			float _IsSectionA;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.localPos = IN.vertex.xyz;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif
					return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;

			fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 color = tex2D(_MainTex, uv);

				#if ETC1_EXTERNAL_ALPHA
					// get the color from an external texture (usecase: Alpha support for ETC1 on android)
					color.a = tex2D(_AlphaTex, uv).r;
				#endif //ETC1_EXTERNAL_ALPHA
					return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 perpendicularPoint = float4(1.0, -1.0/_Slope, 0.0, 1.0);
				//Determine whether or not current point is on same side of line as perpendicular point
				float ax = IN.localPos.x;
				float ay = IN.localPos.y;
				float bx = perpendicularPoint.x;
				float by = perpendicularPoint.y;
				float x1 = 0;
				float y1 = 0;
				float x2 = 1;
				float y2 = _Slope;
				float sameSide = ((y1 - y2) * (ax - x1) + (x2 - x1) * (ay - y1)) * ((y1 - y2) * (bx - x1) + (x2 - x1) * (by - y1));
				if (_Slope < 0) {
					//Workaround to prevent line sides from flipping depending on whether slope is positive or negative
					sameSide *= -1;
				}
				fixed4 c;
				c = SampleSpriteTexture(IN.texcoord) * IN.color;
				c.rgb = lerp(c.rgb, float3(_MultColor.x, _MultColor.y, _MultColor.z), _MultStrength);
				c.rgb *= c.a;
				if (sameSide < 0) {
					if (_IsSectionA) {
						c.rgb = 0;
						c.a = 0;
					}
				}
				else {
					if (!_IsSectionA) {
						c.rgb = 0;
						c.a = 0;
					}
				}
				return c;
			}
			ENDCG
		}
	}
}
