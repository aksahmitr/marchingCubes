#version 330 core

layout (location = 0) out vec4 FragColor;
layout (location = 1) out vec4 BrightColor;

in vec3 trueColor;
in vec3 pos;
in vec3 normal;

uniform vec3 color;

void main()
{

    //pls normalise your colors
    vec3 objectColor = color;

    vec3 final = 10.0f * objectColor;

    
    FragColor = vec4(final, 1.0f);

    //check whether fragment output is higher than threshold, if so output as brightness color
    float brightness = dot(FragColor.rgb, vec3(0.2, 0.2, 0.2));
    if(brightness > 1.0){
        BrightColor = vec4(FragColor.rgb, 1.0);
    }else{
        BrightColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
}