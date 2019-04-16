#version 450 core


layout(location=0) in vec3 VertexPosition;
layout(location=1) in vec2 TexCoord;

layout(location=0) out vec2 OutTexCoord;

void main()
{	
	OutTexCoord = TexCoord;	    
	gl_Position = vec4(VertexPosition.xyz, 1.0);
}