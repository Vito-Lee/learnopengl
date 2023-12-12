#version 330 core
uniform sampler2D texture_height;
uniform samplerCube skybox;
uniform vec3 cameraPos;
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;
in vec2 Texcoords;

void main()
{
    vec3 I = normalize(Position - cameraPos);
    vec3 R = reflect(I, normalize(Normal));
    FragColor = vec4(texture(texture_height,Texcoords).rgb*texture(skybox, R).rgb, 1.0);
}

