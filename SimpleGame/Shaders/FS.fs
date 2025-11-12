#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;

in vec2 v_UV;
uniform float u_Time;

const float c_PI = 3.141592;

void main()
{
    vec2 newPos = v_UV;
    newPos += vec2(0, 0.2 * sin(v_UV.x * c_PI * 2 + u_Time));

    vec4 newColor = texture(u_RGBTexture, newPos);
    FragColor = newColor;

    // FragColor = vec4(v_UV, 0, 1);
}
