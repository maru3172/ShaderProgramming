#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
uniform sampler2D u_NumTexture;

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
    Number();
}
