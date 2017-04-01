// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Battle/Mask"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_MinX("MinX", Float) = -1000
		_MaxX("MaxX", Float) = 1000
		_MinY("MinY", Float) = -1000
		_MaxY("MaxY", Float) = 1000
	}
		SubShader
		{
			Tags {
				"Queue" = "Transparent"
				"RenderType" = "Transparent" }
			LOD 100

			Pass
			{
				ZTest Off
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

			float _MinX;
			float _MaxX;
			float _MinY;
			float _MaxY;
			sampler2D _MyTexture;
			fixed4 _Color;

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color    : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 wpos : TEXCOORD1;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.wpos = worldPos;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color * _Color;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv)*i.color;
			if (i.wpos.x > _MaxX || i.wpos.x < _MinX || i.wpos.y > _MaxY || i.wpos.y < _MinY) {
				col.a = 0;
			}
			return col;
	}
	ENDCG
}
		}
}
