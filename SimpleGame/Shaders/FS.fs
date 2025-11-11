#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;
const float c_PI = 3.141592;

void main()
{
	float vx = 2 * c_PI * v_UV.x * 8; // 0 ~ 2PI, period
	float vy = 2 * c_PI * v_UV.y * 8; // 0 ~ 2PI, period
	// float grayScale = pow(abs(sin(vy) + sin(vx)), 0.5);
	// float grayScale = 1 - pow(abs(sin(vy) + sin(vx)), 0.5);
	float grayScale = 1 - pow(abs(sin(vx)), 0.5);

	vec4 newColor = vec4(grayScale);
	FragColor = newColor;
}
