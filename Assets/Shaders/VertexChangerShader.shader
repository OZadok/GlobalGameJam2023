Shader "Custom/VertexChangerShader"
{
   Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _NoiseLevel ("Noise Level", Range (0,0.2)) = 0.04
        _Seed ("Noise Seed", float) = 0
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Noise.hlsl"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"

            float _NoiseLevel;
            float _Seed;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
            };

            float rand(float n)
            {
                n += _Seed;
                return frac(sin(n) * 43758.5453123);
            }
            
            v2f vert (appdata_base v)
            {
                v2f o;
                
                float3 worldPos = UnityObjectToWorldDir(v.vertex);
                float mult = -1;
                if (rand(worldPos.x) > 0.5)
                {
                    mult = 1;
                }
                float randomNum = rand(worldPos.x + worldPos.y + worldPos.z);
                //v.vertex.xyz += v.normal * randomNum * _NoiseLevel * (randomNum > 0.5 ? 1: -1);
                v.vertex.x += rand(worldPos.x) * _NoiseLevel * (rand(worldPos.x) > 0.5 ? 1: -1);
                v.vertex.y += rand(worldPos.y) * _NoiseLevel * (rand(worldPos.y) > 0.5 ? 1: -1);
                v.vertex.z += rand(worldPos.z) * _NoiseLevel * (rand(worldPos.z) > 0.5 ? 1: -1);
                o.pos = UnityObjectToClipPos(v.vertex);
//                o.pos.x += rand(worldPos.x)/3 * mult;
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));
                // compute shadows data
                TRANSFER_SHADOW(o)
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
                fixed shadow = SHADOW_ATTENUATION(i);
                // darken light's illumination with shadow, keep ambient intact
                fixed3 lighting = i.diff * shadow + i.ambient;
                col.rgb *= lighting;
                return col;
            }
            ENDCG
        }

        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
