#version 330

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

const float c_PI = 3.141592;

void Flag()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = (a_Position.x + 0.5) * 2 * c_PI; // 0 ~ 2PI
	float value1 = a_Position.x + 0.5;

	float dx = 0;
	float dy = value1 * 0.1 * sin(value - u_Time);

	newPosition.y *= (1 - value1 + 0.2);
	newPosition.xy += vec2(dx, dy);

	float shading = (sin(value - u_Time) + 1) / 2 + 0.2;

	gl_Position = newPosition;

	v_Color = vec4(shading);
}

void Wave()
{
	vec4 newPosition = vec4(a_Position, 1);
	gl_Position = newPosition;

	float d = distance(a_Position.xy, vec2(0, 0));

	if(d < 0.5)
	{
		v_Color = vec4(1);
	}
	else
	{
		v_Color = vec4(0);
	}
}

void main()
{
	// Flag();
	Wave();
}
