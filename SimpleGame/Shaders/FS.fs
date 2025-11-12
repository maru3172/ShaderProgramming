#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
in vec2 v_UV;
uniform float u_Time;

// 텍스처 크기를 외부에서 uniform으로 전달하면 정확도가 높음
uniform vec2 u_Resolution;

void main()
{
    // 가우시안 필터 반경
    const int radius = 20;
    // 표준편차(커널 너비)
    const float sigma = 10.0;
    
    // 시그마에 따른 가우시안 분포 상수
    const float invTwoSigma2 = 1.0 / (2.0 * sigma * sigma);
    const float invSigmaRoot = 1.0 / (sqrt(2.0 * 3.14159265) * sigma);

    vec2 texel = 1.0 / u_Resolution;
    vec4 color = vec4(0.0);
    float weightSum = 0.0;

    // 2D 가우시안 필터 (정방향 20픽셀)
    for (int x = -radius; x <= radius; ++x)
    {
        for (int y = -radius; y <= radius; ++y)
        {
            float w = invSigmaRoot * exp(-(float(x * x + y * y)) * invTwoSigma2);
            vec2 offset = vec2(float(x), float(y)) * texel;
            color += texture(u_RGBTexture, v_UV + offset) * w;
            weightSum += w;
        }
    }

    color /= weightSum;
    FragColor = color;
}
