#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 lightPos;
uniform vec3 viewPos;
uniform vec3 objectColor;
uniform vec3 lightColor;
out vec4 fcolor;
void main(){
    gl_Position = projection * view * model * vec4(aPos, 1.0);

    float ambientStrength = 0.1;
    float specularStrength = 1.0;
    vec3 FragPos = vec3(model * vec4(aPos, 1.0));
    vec3 ambient = ambientStrength * lightColor;
    vec3 Normal = mat3(transpose(inverse(model))) * aNormal;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    //world space
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;
    vec3 result = (ambient + diffuse + specular) * objectColor;
    fcolor = vec4(result, 1.0);
}
