#version 330 core
in VS_OUT
{
    vec2 texcoord;
} fs_in;

uniform sampler2D tex; // texture unit from C++
uniform sampler2D norm; // mormal map unit from C++

out vec4 FragColor; // final output

void main()
{
    FragColor = texture(tex, fs_in.texcoord);
}
