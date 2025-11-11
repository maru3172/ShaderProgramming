#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;
uniform float u_Time;

const float c_PI = 3.141592653589793;

// 간단한 해시 (좌표 기반 난수)
float hash(vec2 p)
{
    return fract(sin(dot(p, vec2(127.1, 311.7))) * 43758.5453123);
}

// 소프트 원형 (물방울 본체)
float softCircle(vec2 p, vec2 c, float r, float blur)
{
    float d = length(p - c);
    return smoothstep(r, r - blur, d);
}

// 얇은 링 (파문)
float ring(vec2 p, vec2 c, float r, float w)
{
    float d = length(p - c);
    float a = smoothstep(r + w, r, d);
    float b = smoothstep(r - w, r, d);
    return clamp(a - b, 0.0, 1.0);
}

void main()
{
    vec2 uv = v_UV;
    float time = u_Time * 0.5;

    float color = 0.0;

    // 타일형 반복으로 여러 “물방울” 생성
    vec2 grid = uv * vec2(10.0, 8.0);
    vec2 i = floor(grid);
    vec2 f = fract(grid);

    // 이웃 3x3 타일까지 합성해 자연스럽게
    for (int y = -1; y <= 1; ++y)
    {
        for (int x = -1; x <= 1; ++x)
        {
            vec2 cell = i + vec2(x, y);
            float n = hash(cell);
            // 각 셀마다 물방울 중심, 크기, 속도 난수화
            vec2 c = vec2(cell.x / 10.0, fract(cell.y / 8.0 - time * (0.2 + n * 0.5)));
            float r = 0.05 + n * 0.05;
            vec2 dropCenter = fract(c);

            // 떨어지는 듯한 착시 (y축만 시간에 따라 이동)
            vec2 dropPos = vec2(dropCenter.x, fract(dropCenter.y - time * (0.2 + n)));

            // 본체 + 링
            float body = softCircle(uv, dropPos, r, 0.02);
            float rings = ring(uv, dropPos, r + 0.03 * sin(u_Time + n * 10.0), 0.005);

            color += body * 0.8 + rings * 0.6;
        }
    }

    // 물방울이 아래로 “흘러내리는” 수직 스트릭 추가
    float streak = smoothstep(0.98, 1.0, sin(uv.y * 100.0 + u_Time * 5.0)) * 0.1;

    float gray = clamp(0.1 + color + streak, 0.0, 1.0);
    FragColor = vec4(vec3(gray), 1.0);
}
