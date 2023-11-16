#version 330 core
out vec4 FragColor;
struct Material {
    sampler2D emission;
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};
struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
uniform Light light;
uniform Material material;
uniform float matrixmove;
uniform vec3 viewPos;

in vec2 TexCoords;
in vec3 Normal;
in vec3 FragPos;
void main()
{
    // 环境光
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

    // 漫反射
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));

    // 镜面光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specsampler=vec3(texture(material.specular, TexCoords));
    //反转镜面光贴图
    //specsampler.x=1.f-specsampler.x;
    //specsampler.y=1.f-specsampler.y;
    //specsampler.z=1.f-specsampler.z;
    vec3 specular = light.specular * spec * specsampler;

    vec3 result = vec3(texture(material.emission,vec2(TexCoords.x,TexCoords.y+matrixmove)))+ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
}
