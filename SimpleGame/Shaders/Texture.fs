#version 330

layout(location=0) out vec4 FragColor;

uniform sampler2D u_TexID;
uniform float u_Time;
uniform float u_BloomIntensity = 1.0;
uniform float u_Saturation = 2.0;

in vec2 v_Tex;

vec3 saturate(vec3 c, float s) {
    float l = dot(c, vec3(0.2126, 0.7152, 0.0722));
    return mix(vec3(l), c, s);
}

float hash21(vec2 p) {
    return fract(sin(dot(p, vec2(127.1,311.7))) * 43758.5453123);
}

vec4 applyEffects(sampler2D tex, vec2 uv)
{
    vec4 base = texture(tex, uv);

    float t = u_Time * 0.9;
    float shiftA = 0.012 * (0.5 + 0.5*sin(t*1.25));
    float shiftB = 0.008 * (0.5 + 0.5*cos(t*1.7));
    vec2 dir = normalize(uv - 0.5);
    vec3 chroma;
    chroma.r = texture(tex, uv + dir * shiftA).r;
    chroma.g = texture(tex, uv + vec2(-dir.y, dir.x) * shiftB).g;
    chroma.b = texture(tex, uv - dir * shiftA * 0.6).b;

    vec3 bloomCol = vec3(0.0);
    float bloomWeight = 0.0;
    const int TAP = 8;
    for (int i = 1; i <= TAP; ++i) {
        float fi = float(i);
        float radius = fi * (0.004 + 0.012 * (dot(base.rgb, vec3(0.299,0.587,0.114))));
        float ang = fi * 3.14159 * 0.6 + t * 0.4;
        vec2 offset = vec2(cos(ang), sin(ang)) * radius;
        vec3 sample = texture(tex, uv + offset).rgb;
        float w = 1.0 / fi;
        bloomCol += sample * w;
        bloomWeight += w;
    }
    bloomCol /= max(0.0001, bloomWeight);
    vec3 bloomMix = mix(vec3(0.0), bloomCol, clamp(u_BloomIntensity, 0.0, 1.5));

    float dist = length(uv - 0.5);
    float rim = smoothstep(0.6, 0.35, dist);
    vec3 rimColor = vec3(0.9, 0.6, 1.0) * pow(rim, 1.5);

    float pulse = 0.5 + 0.5 * sin(t * 2.2 + uv.x * 10.0 + uv.y * 6.0);
    vec3 color = chroma + bloomMix * (0.6 + 0.8 * pulse) + rimColor * 0.35;

    float starSeed = hash21(uv * 120.0 + vec2(t * 3.0));
    float sparkle = smoothstep(0.995, 1.0, starSeed);
    color += vec3(pow(sparkle, 12.0)) * 1.6;

    color = saturate(color, clamp(u_Saturation, 0.0, 2.0));
    color = color / (color + vec3(1.0));
    color = pow(color, vec3(1.0/2.2));

    return vec4(color, base.a);
}

void Pixelized()
{
    vec2 newUV = v_Tex;
    float resol = 200 * (sin(u_Time / 3) + 1);
    newUV.x = floor(newUV.x * resol) / resol;
    newUV.y = floor(newUV.y * resol) / resol;
    FragColor = texture(u_TexID, vec2(newUV.x, 1 - newUV.y));
}

void main()
{
    // FragColor = applyEffects(u_TexID, v_Tex);
    // FragColor = vec4(v_Tex, 0, 1);
    // FragColor = texture(u_TexID, vec2(v_Tex.x, 1 - v_Tex.y));
    Pixelized();
}
