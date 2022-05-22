#version 330 core
in VS_OUT
{
    vec3 pos;
    vec3 normal;
    vec2 texcoord;
} fs_in;

uniform sampler2D tex; // texture unit from C++
uniform sampler2D norm; // mormal map unit from C++

uniform vec4 ambient = vec4(43.0/256, 51.0/256, 158.0/256, 1.0)*0.1;
uniform int lightCount = 0; //Max 10
uniform vec3 lightPos[10];
uniform vec4 lightColor[10];

out vec4 FragColor; // final output

vec4 calcLight(){

    vec4 diffuse = vec4(0.0);

    for(int i = 0; i < lightCount; i++){
        vec3 normal = normalize(fs_in.normal);//texture(norm, fs_in.texcoord).xyz;
        normal =  normalize(normal * 2.0 - 1.0);
        vec3 lightDir = normalize(lightPos[i] - fs_in.pos);
        float diff = max(dot(normal, lightDir), 0.0);
        diffuse += diff * lightColor[i];// * lightColor[i].a;
    }

    return (ambient + diffuse);
}

void main()
{
    vec4 color = calcLight();
    color.a = 1;
    FragColor = color * texture(tex, fs_in.texcoord);
}