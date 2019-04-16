#version 450

layout (location = 0) in vec3 VertexPosition;
layout (location = 1) in vec2 VertexTexCoord;


layout (location = 0 ) out vec3 OutPosition;
layout (location = 1 ) out vec2 OutTexCoord;

void main()
{    
	gl_Position = vec4(VertexPosition.xy, 0.0, 1.0);
	OutPosition = vec3(VertexPosition.xy, 0.0);
	OutTexCoord = VertexTexCoord;
}
