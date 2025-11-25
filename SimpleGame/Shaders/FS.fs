#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
uniform sampler2D u_NumTexture;
uniform sampler2D u_TotalNumTexture;
uniform int u_Number;

in vec2 v_UV;
uniform float u_Time;

const float c_PI = 3.141592;

void Test()
{
    vec2 newPos = v_UV;
    newPos += vec2(0, 0.2 * sin(v_UV.x * c_PI * 2 + u_Time));

    vec4 newColor = texture(u_RGBTexture, newPos);
    FragColor = newColor;

    // FragColor = vec4(v_UV, 0, 1);
}

void Circles()
{
    vec2 newUV = v_UV;
    vec2 center = vec2(0.5, 0.5);
    float d = distance(newUV, center);
    vec4 newColor = vec4(0);

    float value = sin(d * 4 * c_PI * 8 - u_Time * 20);

    newColor = vec4(value);
    FragColor = newColor;
}

void Flag()
{
     vec2 newUV = vec2(v_UV.x, (1 - v_UV.y) - 0.5);
     float sinValue = 0.2 * (sin(v_UV.x * 2 * c_PI) + 1) / 2;
     vec4 newColor = vec4(0);
     float width = 0.2;

     if(sinValue + width > newUV.y && sinValue - width < newUV.y)
     {
        newColor = vec4(1);
     }
     else
     {
         discard;
     } 

     FragColor = newColor;
}

void Flag2()
{
     vec2 newUV = vec2(v_UV.x, (1 - v_UV.y) - 0.5);
     float sinValue = v_UV.x * 0.2 * sin(v_UV.x * 2 * c_PI - u_Time * 20);
     vec4 newColor = vec4(0);
     float width = 0.2 * abs(sin((1 - v_UV.x) * 1 * c_PI));

     if(sinValue + width > newUV.y && sinValue - width < newUV.y)
     {
        newColor = vec4(1);
     }
     else
     {
         discard;
     }   

     FragColor = newColor;
}

void Q1()
{
    float newX = v_UV.x;
    float newY = 1 - abs((v_UV.y * 2) - 1);
    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q2()
{
    float newX = fract(v_UV.x * 3);
    float newY = (2 - floor(v_UV.x * 3)) / 3 + v_UV.y / 3;
    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q3()
{
    float newX = fract(v_UV.x * 3);
    float newY = floor(v_UV.x * 3) / 3 + v_UV.y / 3;
    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q4()
{
    float count = 5; // uniform
    float shift = 0.1 * u_Time;
    float newX = fract(fract(v_UV.x * count) + (floor(v_UV.y * count) + 1) * shift);
    float newY = fract(v_UV.y * count);
    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Q5()
{
    float count = 2; // uniform
    float shift = 0.5 * u_Time;
    float newX = fract(v_UV.x * count);
    float newY = fract(fract(v_UV.y * count) + (floor(v_UV.x * count) + 1) * shift);
    FragColor = texture(u_RGBTexture, vec2(newX, newY));
}

void Number()
{
    FragColor = texture(u_NumTexture, v_UV);
}

void TotalNumber()
{
   const float cols = 5.0;
    const float rows = 2.0;

    // 안전한 번호 (0..9)
    int n = clamp(u_Number, 0, 9);
    int col = n % int(cols);   // 0..4
    int row = n / int(cols);   // 0..1

    // 텍스처 픽셀 사이즈 얻기 (mipmap 레벨 0)
    ivec2 texSize = textureSize(u_TotalNumTexture, 0);
    vec2 texSizeF = vec2(texSize);

    // 각 셀의 크기 (텍셀 단위)
    vec2 cellPx = texSizeF / vec2(cols, rows);
    // 화면 UV -> big grid coord
    vec2 big = v_UV * vec2(cols, rows);
    // 셀 내부 local (0..1)
    vec2 local = fract(big);

    // margin (테두리 제거) : 텍셀 단위로 설정하면 정확함
    // 예: 2 픽셀 여백 (필요하면 조절)
    float marginPixels = 2.0;
    // margin을 정규화(0..1) 상대값으로 바꿈 (셀 기준)
    vec2 marginNorm = vec2(marginPixels) / cellPx; // how much fraction of cell to trim on each side

    // 안전한 local 좌표로 리맵: localSafe in [0,1]
    vec2 localSafe = clamp( (local - marginNorm) / (1.0 - marginNorm * 2.0), 0.0, 1.0 );

    // 셀 origin (텍스처 UV 단위)
    vec2 cellSizeUV = vec2(1.0/cols, 1.0/rows);
    // 만약 텍스처 이미지가 위아래 반전되어 있다면 아래 주석을 반전해서 사용
    // float rowF = (rows - 1.0) - float(row); // 세로 반전 필요 시
    float rowF = float(row);

    vec2 cellOrigin = vec2(float(col), rowF) * cellSizeUV;

    // 최종 샘플링 UV
    vec2 texUV = cellOrigin + localSafe * cellSizeUV;

    // (옵션) 텍스처 좌우/상하 추가 변형(예: 흔들림) 예시 (주석 해제하면 작동)
    // float wobble = 0.005 * sin(u_Time * 2.0 * c_PI);
    // texUV.x += wobble * 0.5;
    // texUV.y += wobble * 0.2;

    vec4 color = texture(u_TotalNumTexture, texUV);

    // 알파/투명도 처리 필요하면 추가 (현재 그대로 출력)
    FragColor = color;
}

void main()
{
    //Test();
    //Circles();
    //Flag();
    // Flag2();
    // Q1();
    // Q2();
    // Q3();
    // Q4();
    // Q5();
    TotalNumber();
}
