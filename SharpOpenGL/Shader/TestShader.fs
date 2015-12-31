
#version 430 core

in vec3 Color;
in vec2 OutTexCoord;
out vec4 FragColor;

uniform sampler2D TestTexture;

void main()
{

    //FragColor = vec4(Color, 1.0);
    FragColor = texture(TestTexture, OutTexCoord);
}