#version 450 core

uniform ModelTransform
{
	mat4x4 Model;
};

uniform CameraTransform
{
	mat4x4 View;
	mat4x4 Proj;
};

uniform PrevTransform
{
	mat4 PrevProj;
	mat4 PrevModel;
	mat4 PrevView;	
};

uniform mat4 NormalMatrix;

uniform vec3 Value;


layout(location=0) in vec3 VertexPosition;
layout(location=1) in vec3 VertexNormal;
layout(location=2) in vec2 TexCoord;
layout(location=3) in vec4 Tangent;


layout(location=0) out vec4 OutPosition;
layout(location=1) out vec2 OutTexCoord;
layout(location=2) out vec3 OutNormal;
layout(location=3) out vec3 OutTangent;
layout(location=4) out vec3 OutBinormal;
layout(location=5) out vec3 NDCPos;
layout(location=6) out vec3 PrevNDCPos;

  
void main()
{	
	mat4 ModelView = View * Model;

	OutTexCoord = TexCoord;
	gl_Position = Proj * View * Model * vec4(VertexPosition, 1);
	OutPosition =   (ModelView * vec4(VertexPosition, 1));
	
	OutNormal =  normalize(mat3(ModelView) * VertexNormal);	

	OutTangent = normalize(mat3(ModelView) * vec3(Tangent));

	vec3 binormal = (cross( VertexNormal, Tangent.xyz )) * Tangent.w;
	OutBinormal = normalize(mat3(ModelView) * binormal);	

	NDCPos = gl_Position.xyz / gl_Position.w;
	vec4 PrevPos = PrevProj * PrevView * PrevModel * vec4(VertexPosition,1.0);
	PrevNDCPos = PrevPos.xyz / PrevPos.w;
}