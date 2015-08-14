float4 ColorTone : register(C0);

sampler2D Input1 : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(Input1, uv);
    float gray = dot(color.rgb, float3(0.2126, 0.7152, 0.0722));
    color *= ColorTone;
    color.a -= gray;
    return color;
}