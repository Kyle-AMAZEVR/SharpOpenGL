using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Core;
using Core.Buffer;
using Core.OpenGLShader;
using Core.Texture;
using Core.VertexCustomAttribute;
namespace SharpOpenGL.BasicMaterial
{


[StructLayout(LayoutKind.Explicit,Size=16)]
public struct ColorBlock
{
	[FieldOffset(0)]
	public OpenTK.Vector3 Value;
}


[StructLayout(LayoutKind.Explicit,Size=192)]
public struct Transform
{
	[FieldOffset(0)]
	public OpenTK.Matrix4 Model;
	[FieldOffset(64)]
	public OpenTK.Matrix4 View;
	[FieldOffset(128)]
	public OpenTK.Matrix4 Proj;
}

}