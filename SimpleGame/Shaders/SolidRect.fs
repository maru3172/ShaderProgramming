#version 330

in vec4 v_Color;

layout(location=0) out vec4 FragColor;

uniform vec4 u_Color;

const float c_PI = 3.141592;

void discn()
{
    // FragColor = vec4(u_Color.r, u_Color.g, u_Color.b, u_Color.a);

	if(v_Color.b < 0.5 && v_Color.r < 0.5)
		FragColor = v_Color;
	else
		discard;
}

void FilledCircle()
{
	vec4 newColor = vec4(1, 1, 1, 1,);
	float r = 0.5;
	vec2 center = vec2(0.5, 0.5);
	float dist = distance(v_Color.rg, center);
	if(dist < r)
	{
		newColor = vec4(1, 1, 1, 1);
	}
	else
	{
		newColor = vec4(0, 0, 0, 0);
	}
	FragColor = newColor;
}

void Circle()
{
	vec4 newColor = vec4(1, 1, 1, 1);
	float r = 0.5;
	float width = 0.05;
	vec2 center = vec2(0.5, 0.5);
	float dist = distance(v_Color.rg, center);
	if(dist < r - width && dist < r)
	{
		newColor = vec4(1, 1, 1, 1);
	}
	else
	{
		newColor = vec4(0, 0, 0, 0);
	}
	FragColor = newColor;
}

void Circles()
{
	float circleCount = 10.0; // 0 ~ 1
	vec2 circleCenter = vec2(0.5, 0.5);
	float maxDist = 0.5;
	float dist = distance(v_Color.rg, circleCenter);
	float input = (circleCount * 2 * c_PI + c_PI) * 0.5 * dist/maxDist;
	float sinValue = pow(sin(input), 256);
	FragColor = vec4(sinValue);
}

void Circles2()
{
	float circleCount = 10.0; // 0 ~ 1
	vec2 circleCenter = vec2(0.5, 0.5);
	float dist = distance(v_Color.rg, circleCenter);
	float input = circleCount * c_PI * 4 * dist;
	float sinValue = pow(sin(input), 16);
	FragColor = vec4(sinValue);
}

void sinGraph()
{
	vec2 newTexPos = vec2(v_Color.r * 2 * c_PI, v_Color.g * 2 - 1);
	float period = 2;
	float amp = 0.2;
	float speed = 1;
	// float sinValue = amp * sin(newTexPos.x * period * speed) - amp;
	float sinValue = v_Color.r * amp * sin(newTexPos.x * period);
	float width = 0.2;
	width = width * (1 - v_Color.r);
	if(sinValue < newTexPos.y + width && sinValue > newTexPos.y - width)
	{
		FragColor = vec4((sinValue + 1) / 2 - 0.2);
	}
	else
	{
		FragColor = vec4(0);
	}
}

void main()
{
	// discn()
}
