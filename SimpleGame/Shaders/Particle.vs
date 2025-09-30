#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;
in float a_STime;
in vec3 a_Vel;
in float a_LifeTime;
in float a_Mass;

out vec4 v_Color;

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
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1;
	float amp = a_Value * 2 - 1.f;
	float period = 2.f;

	float newTime = u_Time - a_STime;

	if(newTime > 0){
		float t = fract(newTime / a_LifeTime) * a_LifeTime ;
		float tt = t * t;
		float nTime = t / a_LifeTime;
		float x = nTime * 2.f - 1.f; // -1 ~ 1
		float y = nTime * amp * sin(period * (2.f * c_PI * (t / a_LifeTime)));

		newPosition.xy += vec2(x, y);
		newAlpha = 1 - t / a_LifeTime;
	}
	else{
		newPosition.xy += vec2(-100000, 0);
	}

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void main()
{
	// fountain();
	sinParticle();
}
