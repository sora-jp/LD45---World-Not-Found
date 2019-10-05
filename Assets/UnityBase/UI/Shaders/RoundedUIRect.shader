Shader "Unlit/RoundedUIRect"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Radius ("Radius", Float) = 0.5
		_Aspect("Aspect Ratio", Float) = 0.5
		[MaterialToggle]_IgnoreRadius("Ignore Radius", Float) = 0

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15
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

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

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
				float2 data : TEXCOORD1;
				float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 color : TEXCOORD1;
				float2 data : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Radius, _Aspect, _IgnoreRadius;

			float2 closest(float2 a, float2 b, float2 p) 
			{
				float2 orig = p - a;
				float2 v = normalize(b - a);
				float puvLen = clamp(dot(orig, v), 0, length(b-a));
				return v * puvLen + a;
			}

			float sqrlen(float2 a) 
			{
				return dot(a, a);
			}

			float linedst(float2 a, float2 b, float2 p) 
			{
				return sqrlen(closest(a, b, p) - p);
			}

			float sampleRect(float2 p, float radius, float ar) 
			{
				if (radius >= 0.5 || _IgnoreRadius) 
				{
					if (ar != 1) return linedst(float2(0.5, 0.5), float2(ar - 0.5, 0.5), float2(p.x * ar, p.y)) < 0.25;
					return sqrlen(p - float2(0.5, 0.5)) < 0.25;
				}
				if (p.x * ar > radius && p.x * ar < ar -radius && p.y > radius && p.y < 1-radius) return 1;

				float2 bl = float2(radius, radius);
				float2 tr = float2(ar - radius , 1 - radius);
				float2 br = float2(bl.x, tr.y);
				float2 tl = float2(tr.x, bl.y);

				float2 pp = float2(p.x * ar, p.y);

				float dst = linedst(bl, br, pp);
				dst = min(dst, linedst(tl, tr, pp));
				dst = min(dst, linedst(tl, bl, pp));
				dst = min(dst, linedst(tr, br, pp));

				return dst < radius * radius;
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
				o.data = v.data;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				float o = 0.25 / i.data.y;
				float o2 = (0.25 / i.data.y) / i.data.x;
				float alpha = sampleRect(i.uv + float2(o2, o), _Radius / i.data.y, i.data.x);
				alpha += sampleRect(i.uv + float2(-o2, o), _Radius / i.data.y, i.data.x);
				alpha += sampleRect(i.uv + float2(o2, -o), _Radius / i.data.y, i.data.x);
				alpha += sampleRect(i.uv + float2(-o2, -o), _Radius / i.data.y, i.data.x);
				return float4(col.rgb, alpha * 0.25 * col.a);
            }
            ENDCG
        }
    }
}
