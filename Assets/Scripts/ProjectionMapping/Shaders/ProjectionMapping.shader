Shader "ProjectionMapping/ProjectionMapping" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		matrixRow_1 ("matrixRow_1", Vector) = (1, 0, 0, 0)
		matrixRow_2 ("matrixRow_2", Vector) = (0, 1, 0, 0)
		matrixRow_3 ("matrixRow_3", Vector) = (0, 0, 1, 0)
		matrixRow_4 ("matrixRow_4", Vector) = (0, 0, 0, 1)
	}
		
	SubShader {
		Pass {
			Lighting Off
						
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
            #pragma target 3.0
            #include "HLSLSupport.cginc"
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 matrixRow_1;
			float4 matrixRow_2;
			float4 matrixRow_3;
			float4 matrixRow_4;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 packuv0 : TEXCOORD0;
			};

			float4 _MainTex_ST;
			
			v2f vert (appdata_full v)
			{	
				float4x4 matrixH = { matrixRow_1.x, matrixRow_1.y, matrixRow_1.z, matrixRow_1.w,
								  matrixRow_2.x, matrixRow_2.y, matrixRow_2.z, matrixRow_2.w,
								  matrixRow_3.x, matrixRow_3.y, matrixRow_3.z, matrixRow_3.w,
								  matrixRow_4.x, matrixRow_4.y, matrixRow_4.z, matrixRow_4.w };

				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP,  v.vertex);
				o.pos = o.pos / o.pos.w;
				o.pos = mul (matrixH, o.pos);
				o.pos.x = o.pos.x / o.pos.w;
				o.pos.y = o.pos.y / o.pos.w;
				o.pos.w = o.pos.w / o.pos.w;
			    o.packuv0.xy = TRANSFORM_TEX (v.texcoord, _MainTex);
				return o;
			}
			
			half4 frag (v2f IN) : COLOR
			{
				float2 uv_MainTex = IN.packuv0.xy;
                half4 c = tex2D(_MainTex, uv_MainTex);
                return c;
			}

			ENDCG
		}
	}
	 
	FallBack "Diffuse"
}
