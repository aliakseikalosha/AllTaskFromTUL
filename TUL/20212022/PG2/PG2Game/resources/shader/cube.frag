#version 410 core
in VS_OUT
{
    vec3 N;
    vec4 P;
    vec3 V;
} fs_in;

uniform sampler2D tex; // texture unit from C++
uniform sampler2D norm; // mormal map unit from C++

uniform vec4 ambient = vec4(43.0/256, 51.0/256, 158.0/256, 1.0)*0.3;
uniform int lightCount = 0; //Max 10
uniform vec3 lightPos[10];
uniform vec4 lightColor[10];

out vec4 FragColor; // final output

vec4 calcLight(){

    vec4 diffuse = vec4(0.0);
    vec3 N = normalize(fs_in.N);
    vec3 V = normalize(fs_in.V);
    for(int i = 0; i < lightCount; i++){
        vec3 C = max(dot(N, normalize(lightPos[i]+V)), 0.0) * vec3(1.0);//max(dot(N, normalize(lightPos[i] + fs_in.V)), 0.0);
        diffuse+=vec4(C, 1.0) * lightColor[i];
    }
    diffuse.a = 1;
    return (ambient + diffuse);
}

void main()
{
    FragColor = calcLight();
}
