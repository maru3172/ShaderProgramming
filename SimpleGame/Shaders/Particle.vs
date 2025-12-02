#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;
in float a_STime;
in vec3 a_Vel;
in float a_LifeTime;
in float a_Mass;
in float a_Period;
in vec2 a_Tex;

out vec4 v_Color;
out vec2 v_Tex;

uniform float u_Time;
uniform vec3 u_Force;

const float c_PI = 3.141592;
const vec2 c_G = vec2(0, -9.8);

void fountain()
{
	float lifeTime = a_LifeTime;
	float newAlpha = 1;
	float newTime = u_Time - a_STime;
	vec4 newPosition = vec4(a_Position, 1);
	if(newTime > 0)
	{
		float fX = c_G.x * a_Mass + u_Force.x * 2;
		float fY = c_G.y * a_Mass + u_Force.y * 2;
		float aX = fX / a_Mass;
		float aY = fY / a_Mass;
		float t = fract(newTime / lifeTime) * lifeTime * a_Mass;
		float tt = t * t;
		float x =  a_Vel.x * t + 0.5 * aX * tt;
		float y =  a_Vel.y * t + 0.5 * aY * tt;
		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / lifeTime;
	}
	else
	{
		newPosition.xy -= vec2(-100000, 0);
	}
	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void sinParticle()
{
	vec4 centerColor = vec4(1, 0, 0, 1);
	vec4 borderColor = vec4(1, 1, 1, 0);
	vec4 newColor = a_Color;
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1;
	float amp = a_Value * 2 - 1.f;
	float period = a_Period * 2.f;

	float newTime = u_Time - a_STime;

	if(newTime > 0){
		float t = fract(newTime / a_LifeTime) * a_LifeTime ;
		float tt = t * t;
		float nTime = t / a_LifeTime;
		float x = nTime * 2.f - 1.f; // -1 ~ 1
		float y = nTime * sin(nTime * c_PI) * amp * sin(period * (2.f * c_PI * (t / a_LifeTime)));

		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / a_LifeTime;

		float d = abs(y);
		newColor = mix(centerColor, borderColor, d * 20);
	}
	else{
		newPosition.xy += vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = vec4(newColor.rgb, newAlpha);
}

void circleParticle()
{
	vec4 newPosition = vec4(a_Position, 1);
	newPosition.xy *= 3;
	float newAlpha = 1;
	float lifeTime = a_LifeTime;
	float newTime = u_Time - a_STime;
	float newValue = a_Value * 2 * c_PI;

	if(newTime > 0){
		float x = sin(newValue);
		float y = cos(newValue);

		float t = fract(newTime / a_LifeTime) * a_LifeTime;
		float tt = t * t;

		float newX = x + 0.5 * c_G.x * tt;
		float newY = y + 0.5 * c_G.y * tt;

		newPosition.xy += vec2(newX, newY);
		newAlpha = 1 - t / a_LifeTime;
	}
	else{
		newPosition.xy += vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb * 3 * newAlpha, newAlpha);
}

// 중간고사
void Q1()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2; // 0 ~ 2PI
	float dx = 2 * (a_Value - 0.5);
	float dy = 0.5 * sin(value - u_Time);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void Q2()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2; // 0 ~ 2PI
	float dx = sin(value);
	float dy = fract(u_Time) * cos(value);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void Q3()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2; // 0 ~ 2PI
	float dx = a_Value * sin(value * 4 + u_Time);
	float dy = a_Value * cos(value * 4 + u_Time);

	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void main()
{
	// fountain();
	// sinParticle();
	circleParticle();
	// Q1();
	// Q2();
	// Q3();

	v_Tex = a_Tex;
}
