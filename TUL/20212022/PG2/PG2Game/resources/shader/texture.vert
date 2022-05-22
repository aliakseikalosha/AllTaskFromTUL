#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;
layout (location = 2) in vec3 aNormal;

uniform mat4 uProj_m, uV_m, uM_m;
out VS_OUT
{
    vec3 pos;
    vec3 normal;
    vec2 texcoord;
} vs_out;



void main()
{
    gl_Position = uProj_m * uV_m * uM_m * vec4(aPos, 1.0f);
    vs_out.texcoord = aTex;
    vs_out.pos = aPos;
    vs_out.normal = aNormal;
}
