sampler2D Input1 : register(S0);
float3 mul = float3(0.2126, 0.7152, 0.0722);
float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 clr = tex2D(Input1, uv);
    return dot(clr.rgba, float3(0.2126, 0.7152, 0.0722) * .5);
}