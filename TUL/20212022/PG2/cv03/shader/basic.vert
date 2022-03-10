#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 uProj_m, uM_m, uV_m;

void main()
{
    // Outputs the positions/coordinates of all vertices
    //gl_Position =  uProj_m * uV_m * uM_m * vec4(aPos, 1.0f);
    gl_Position =  vec4(aPos, 1.0f);//uProj_m * uV_m * uM_m * vec4(aPos, 1.0f);
}