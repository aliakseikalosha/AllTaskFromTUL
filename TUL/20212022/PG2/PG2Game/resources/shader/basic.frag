#version 330 core

uniform sampler2D tex; // texture unit from C++
uniform sampler2D norm; // mormal map unit from C++
uniform vec4 color;

out vec4 FragColor;

void main()
{
    //FragColor = vec4(gl_FragCoord.x/400,gl_FragCoord.y/400,gl_FragCoord.z/400,1.0f);
    FragColor = color;
}