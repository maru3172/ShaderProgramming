#version 330

in vec4 v_Color;
in vec2 v_UV;

layout(location=0) out vec4 FragColor;

void main()
{
	FragColor = v_Color;
	// FragColor = vec4(v_UV, 0, 1);
}
