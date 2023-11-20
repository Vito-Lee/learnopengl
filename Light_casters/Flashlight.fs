#version 330 core
out vec4 FragColor;
struct Material {
    //sampler2D emission;
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};
struct Light {
    vec3  position;
    vec3  direction;
    float cutOff;
    float outerCutOff;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

};
uniform Light light;
uniform Material material;
//uniform float matrixmove;
uniform vec3 viewPos;

in vec2 TexCoords;
in vec3 Normal;
in vec3 FragPos;
void main()
{



    // ambient
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;

    // diffuse
    vec3 norm = normalize(Normal);
    // vec3 lightDir = normalize(light.position - FragPos);
    vec3 lightDir = normalize(light.position-FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;

    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;


    float theta     = dot(lightDir, normalize(-light.direction));
    float epsilon   = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

    diffuse*=intensity;
    specular*=intensity;

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);

}
