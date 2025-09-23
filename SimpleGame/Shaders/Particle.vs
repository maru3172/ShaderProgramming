#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;

out vec4 v_Color;

uniform float u_Time;

const float c_PI = 3.141592;
const vec2 c_G = vec2(0, -9.8);

void main()
{
	float t = fract(u_Time);
	float tt = t * t;
	float x = 0, y = 0.5 * c_G.y * tt;
	vec4 newPosition = vec4(a_Position, 1);
	newPosition.xy += vec2(x, y);
	gl_Position = newPosition;

	v_Color = a_Color;
}
