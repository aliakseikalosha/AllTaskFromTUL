#version 410 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;
layout (location = 2) in vec3 aNormal;

uniform mat4 uProj_m, uV_m, uM_m;
out VS_OUT
{
    vec3 N;
    vec4 P;
    vec3 V;
} vs_out;


void main(void)
{ // Create Model-View matrix
    mat4 mv_m = uV_m * uM_m;
    // Calculate view-space coordinate - in P point we are computing the color
    vec4 P = mv_m * vec4(aPos, 1.0);
    // Calculate normal in view space
    vs_out.N = mat3(mv_m) * aNormal;
    // Calculate view-space light vector
    vs_out.P = P;
    // Calculate view vector (negative of the view-space position)
    vs_out.V = -P.xyz;
    // Calculate the clip-space position of each vertex
    gl_Position = uProj_m * P;
}
