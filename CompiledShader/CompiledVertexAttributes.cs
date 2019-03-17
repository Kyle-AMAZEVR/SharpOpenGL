using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Core;
using Core.Buffer;
using Core.OpenGLShader;
using Core.Texture;
using Core.VertexCustomAttribute;
using Core.MaterialBase;
using ZeroFormatter;
using ZeroFormatter.Formatters;
using Core.CustomAttribute;
namespace SharpOpenGL
{
namespace BasicMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace SimpleMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=12)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, new IntPtr(0));
	}
}
}
namespace ScreenSpaceDraw
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace GBufferDraw
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=48)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
		
	[Index(3)]
	[FieldOffset(32), ComponentCount(4), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector4 Tangent;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 48, new IntPtr(24));
		GL.EnableVertexAttribArray(3);
		GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 48, new IntPtr(32));
	}
}
}
namespace GBufferInstanced
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=48)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
		
	[Index(3)]
	[FieldOffset(32), ComponentCount(4), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector4 Tangent;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 48, new IntPtr(24));
		GL.EnableVertexAttribArray(3);
		GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 48, new IntPtr(32));
	}
}
}
namespace GBufferWithoutTexture
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=48)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
		
	[Index(3)]
	[FieldOffset(32), ComponentCount(4), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector4 Tangent;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 48, new IntPtr(24));
		GL.EnableVertexAttribArray(3);
		GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 48, new IntPtr(32));
	}
}
}
namespace GBufferDump
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace GBufferPNC
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=36)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexColor;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 36, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 36, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 36, new IntPtr(24));
	}
}
}
namespace GBufferCubeTest
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=24)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(12));
	}
}
}
namespace GBufferPNCT
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=44)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexColor;
		
	[Index(3)]
	[FieldOffset(36), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 44, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 44, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 44, new IntPtr(24));
		GL.EnableVertexAttribArray(3);
		GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, 44, new IntPtr(36));
	}
}
}
namespace Blur
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace BloomMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace LightMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace CubemapMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace MSGBufferMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=48)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexNormal;
		
	[Index(2)]
	[FieldOffset(24), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
		
	[Index(3)]
	[FieldOffset(32), ComponentCount(4), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector4 Tangent;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 48, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 48, new IntPtr(24));
		GL.EnableVertexAttribArray(3);
		GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 48, new IntPtr(32));
	}
}
}
namespace DepthVisualizeMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace FontRenderMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace FontBoxRenderMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace GridRenderMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=12)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12, new IntPtr(0));
	}
}
}
namespace ThreeDTextRenderMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 TexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace ResolveMaterial
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=20)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, new IntPtr(12));
	}
}
}
namespace SSAO
{


[ZeroFormattable]
[StructLayout(LayoutKind.Explicit,Size=32)]
public struct VertexAttribute
{
	
	[Index(0)]
	[FieldOffset(0), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 VertexPosition;
		
	[Index(1)]
	[FieldOffset(12), ComponentCount(2), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector2 VertexTexCoord;
		
	[Index(2)]
	[FieldOffset(20), ComponentCount(3), ComponentType(VertexAttribPointerType.Float)]
	public OpenTK.Vector3 FrustumVector;
	
	public static void VertexAttributeBinding()
	{
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 32, new IntPtr(0));
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 32, new IntPtr(12));
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 32, new IntPtr(20));
	}
}
}

}
