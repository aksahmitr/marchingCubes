#version 330 core
layout (location = 0) out vec4 FragColor;
  
in vec2 TexCoords;

uniform sampler2D hdrBuffer;
uniform sampler2D bloomBuffer;
//uniform float exposure;

void main()
{
    const float gamma = 1.0; //2.2
    vec3 hdrColor = texture(hdrBuffer, TexCoords).rgb;
    vec3 bloomColor = texture(bloomBuffer,TexCoords).rgb;

    hdrColor+=bloomColor;
    
    // exposure tone mapping
    vec3 mapped = vec3(1.0) - exp(-hdrColor * 1.0f);

    // gamma correction 
    mapped = pow(mapped, vec3(1.0 / gamma));
  
    FragColor = vec4(mapped, 1.0);
}  