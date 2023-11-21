#version 330 core
struct PointLight {
    vec3 position;

    float constant;
    float linear;
    float quadratic;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
#define NR_POINT_LIGHTS 2
uniform PointLight pointLights[NR_POINT_LIGHTS];
uniform sampler2D texture_diffuse1;
uniform vec3 viewPos;

out vec4 FragColor;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;


void main()
{
    vec4 fcolor=texture(texture_diffuse1,TexCoords);
    vec4 result=fcolor;
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragPos);
    for(int i=0;i<2;i++){
        float distance    = length(pointLights[i].position - FragPos);
        float attenuation = 1.0 / (pointLights[i].constant + pointLights[i].linear * distance +
                 pointLights[i].quadratic * (distance * distance));
        result+=fcolor*attenuation;


    }
    FragColor = result;
}
