#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;
layout (location = 2) in vec3 aNormal;

uniform mat4 uProj_m, uM_m, uV_m;
uniform vec3 worldPos;

out VS_OUT
{
    vec3 pos;
    vec3 normal;
    vec2 texcoord;
} vs_out;

void main()
{
    vec3 CameraRight_worldspace = vec3( uV_m[0][0], uV_m[1][0], uV_m[2][0]);
    vec3 CameraUp_worldspace = vec3(uV_m[0][1], uV_m[1][1], uV_m[2][1]);

    vec3 particleCenter_wordspace = worldPos;

    vec3 vertexPosition_worldspace =
    particleCenter_wordspace
    + CameraRight_worldspace * aPos.x
    + CameraUp_worldspace * aPos.y;


    // Output position of the vertex
    gl_Position = uProj_m * uV_m * vec4(vertexPosition_worldspace, 1.0f);

    vs_out.texcoord = aTex;
    vs_out.pos = vertexPosition_worldspace;
    vs_out.normal = aNormal;
}