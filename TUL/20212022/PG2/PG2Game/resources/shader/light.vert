#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 uProj_m, uM_m, uV_m;
uniform vec3 worldPos;

void main()
{
    vec3 CameraRight_worldspace = vec3( uV_m[0][0], uV_m[1][0], uV_m[2][0]);
    vec3 CameraUp_worldspace = vec3(uV_m[0][1], uV_m[1][1], uV_m[2][1]);

    vec3 particleCenter_wordspace = worldPos;

    vec3 vertexPosition_worldspace =
    particleCenter_wordspace
    + CameraRight_worldspace * aPos.x
    + CameraUp_worldspace * aPos.y;
    vertexPosition_worldspace.y-=1;


    // Output position of the vertex
    gl_Position = uProj_m * uV_m * vec4(vertexPosition_worldspace, 1.0f);
}