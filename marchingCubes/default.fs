#version 330 core

layout (location = 0) out vec4 FragColor;
layout (location = 1) out vec4 BrightColor;

in vec3 trueColor;
in vec3 pos;
in vec3 normal;

uniform vec3 dir;

uniform float cutoff;

#define POINT_LIGHT_NUMS 3

struct PointLight{
    vec3 position;
    vec3 color; //goes to diffuse

    //constants
    float constant;
    float linear;
    float quadratic;
};

uniform PointLight lights[POINT_LIGHT_NUMS];

vec3 pointLightCalc(PointLight light, vec3 normal, vec3 objectColor,float ambient);

void main()
{
    vec3 norm = normalize(normal);
    vec3 Ndir = normalize(dir);
    //pls normalise your colors
    vec3 objectColor = vec3(((pos.x/cutoff + (1-pos.z/cutoff))/2.0f),(1-pos.x/cutoff + (1-pos.z/cutoff))/2.0f,1.0f * pos.z/cutoff);

    vec3 lightColor = vec3(1.0f,1.0f,1.0f);
    float diffuse = max(0.0f,dot(norm,Ndir));
    float ambientStrength = 0.2f;
    vec3 ambient = ambientStrength * lightColor;

    vec3 final = (ambient+(diffuse*0.1f)) * objectColor;

    for(int i=0;i<POINT_LIGHT_NUMS;i++){
        //PointLight bulb={lights[i],vec3(1.0f,1.0f,1.0f),1.0f,0.0022f,0.0019f};
        final += 5.0f * pointLightCalc(lights[i],norm,objectColor,0.25f);
    }

    
    FragColor = vec4(final, 1.0f);

    //check whether fragment output is higher than threshold, if so output as brightness color
    float brightness = dot(FragColor.rgb, vec3(0.2, 0.2, 0.2));
    if(brightness > 1.0){
        BrightColor = vec4(FragColor.rgb, 1.0);
    }else{
        BrightColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
}

vec3 pointLightCalc(PointLight light, vec3 normal, vec3 objectColor,float ambient){
    vec3 dir = normalize(light.position - pos);
    float dist = length(light.position - pos);
    float attenuation = 1.0 / (light.constant + light.linear * dist + light.quadratic * (dist * dist));
    return (ambient + max(dot(dir,normal),0.0f))*objectColor*light.color*attenuation;
}