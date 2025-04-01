Shader "Custom/MultiFaceShader"
{
    Properties
    {
        _ColorTop ("Top Color", Color) = (1,1,0,1) // 黄色
        _ColorBottom ("Bottom Color", Color) = (1,0.4,1,1) // 粉色
        _ColorLeft ("Left Color", Color) = (0.3,1,0.3,1) // 绿色
        _ColorRight ("Right Color", Color) = (0.3,0.3,1,1) // 蓝色
        _ColorFront ("Front Color", Color) = (0.7,0.5,0.2,1) // 棕色
        _ColorBack ("Back Color", Color) = (1,0.2,0.2,1) // 红色
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normalDir : TEXCOORD0;
            };

            fixed4 _ColorTop, _ColorBottom, _ColorLeft, _ColorRight, _ColorFront, _ColorBack;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normalDir = normalize(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (i.normalDir.y > 0.5) return _ColorTop;
                if (i.normalDir.y < -0.5) return _ColorBottom;
                if (i.normalDir.x < -0.5) return _ColorLeft;
                if (i.normalDir.x > 0.5) return _ColorRight;
                if (i.normalDir.z > 0.5) return _ColorFront;
                if (i.normalDir.z < -0.5) return _ColorBack;
                return fixed4(1, 1, 1, 1); // fallback color
            }
            ENDCG
        }
    }
}
