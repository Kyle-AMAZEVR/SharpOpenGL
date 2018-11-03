﻿using System;
using System.Collections.Generic;
using Core;
using Core.Buffer;
using Core.MaterialBase;
using Core.Primitive;
using Core.Texture;
using FreeImageAPI.Metadata;
using OpenTK;
using SixLabors.Fonts;
using SixLabors.Primitives;
using SharpOpenGL;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.Primitives;


namespace SharpOpenGL.Font
{
    public class TextInstance : IDisposable
    {
        public TextInstance(string textContent, float x, float y, int fontSize)
        {
            this.fontSize = fontSize;
            TextContent = textContent;
            GenerateVertices(x,y);
        }

        public void Dispose()
        {
            if (vb != null)
            {
                vb.Dispose();
            }
        }

        public void RenderWithBox(MaterialBase fontRenderMaterial, MaterialBase fontBoxRenderMaterial)
        {
            RenderBox(fontBoxRenderMaterial);
            Render(fontRenderMaterial);
        }

        private void RenderBox(MaterialBase fontBoxRenderMaterial)
        {
            using (var blend = new ScopedEnable(EnableCap.Blend))
            using (var dummy = new ScopedDisable(EnableCap.DepthTest))
            using (var blendFunc = new ScopedBlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha))
            {
                fontBoxRenderMaterial.BindAndExecute(boxVB, () =>
                {
                    boxVB.BindVertexAttribute();
                    fontBoxRenderMaterial.SetUniformVarData("ScreenSize",
                        new OpenTK.Vector2(OpenGLContext.Get().WindowWidth, OpenGLContext.Get().WindowHeight));
                    fontBoxRenderMaterial.SetUniformVarData("BoxColor", new Vector3(0,0,0));
                    fontBoxRenderMaterial.SetUniformVarData("BoxAlpha", 0.70f);
                    GL.DrawArrays(PrimitiveType.Quads, 0, 4);
                });
            }
        }

        public void Render(MaterialBase fontRenderMaterial)
        {
            using (var blend = new ScopedEnable(EnableCap.Blend))
            using (var dummy = new ScopedDisable(EnableCap.DepthTest))
            using (var sampler = new Sampler())
            using (var blendFunc = new ScopedBlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha))
            {
                fontRenderMaterial.BindAndExecute(vb, () =>
                {   
                    vb.BindVertexAttribute();
                    fontRenderMaterial.SetTexture("FontTexture", FontManager.Get().FontAtlas);
                    fontRenderMaterial.SetUniformVarData("ScreenSize",
                        new OpenTK.Vector2(OpenGLContext.Get().WindowWidth, OpenGLContext.Get().WindowHeight));
                    GL.DrawArrays(PrimitiveType.Quads, 0, vertexList.Count);
                });
            }
        }



        protected void GenerateVertices(float originX, float originY)
        {
            vertexList.Clear();

            var fXBasePosition = originX;

            var squareSize = FontManager.Get().SquareSize;
            var textureDimension = FontManager.Get().TextureDimension;

            int index = 0;
            ulong previous = 0;

            foreach (var ch in TextContent)
            {
                if (FontManager.Get().GlyphDictionary.ContainsKey(ch) == false)
                {
                    continue;
                }

                long kerning = 0;
                if (index > 0)
                {
                    kerning = FontManager.Get().GetKerning(previous, ch);
                }

                var fScale = 1.0f;
                var glyph = FontManager.Get().GlyphDictionary[ch];

                fXBasePosition += ((glyph.AdvanceHorizontal ) / (2.0f * 64.0f)) * fScale;
                float X = fXBasePosition - (glyph.Width / (2.0f * 64.0f)) * fScale;
                float Y = originY + ((glyph.HoriBearingY) / (2.0f * 64.0f)) * fScale;

                var halfSquare = (squareSize / 2.0f) * fScale;

                var leftX = -0.5f * halfSquare + X;
                var rightX = 0.5f * halfSquare + X;
                var topY = 0.5f * halfSquare + Y;
                var bottomY = -0.5f * halfSquare + Y;

                // update boundary
                if (left > leftX)
                {
                    left = leftX;
                }
                if (right < rightX)
                {
                    right = rightX;
                }

                if (top < topY)
                {
                    top = topY;
                }

                if (bottom > bottomY)
                {
                    bottom = bottomY;
                }

                var charvertex1 = new OpenTK.Vector3( leftX, topY, 0);
                var charvertex2 = new OpenTK.Vector3( rightX, topY, 0);
                var charvertex3 = new OpenTK.Vector3( rightX, bottomY, 0);
                var charvertex4 = new OpenTK.Vector3( leftX, bottomY, 0);
                
                
                var texcoord1 = new OpenTK.Vector2(glyph.AtlasX, glyph.AtlasY);
                var texcoord2 = new OpenTK.Vector2(glyph.AtlasX + textureDimension, glyph.AtlasY);
                var texcoord3 = new OpenTK.Vector2(glyph.AtlasX + textureDimension, glyph.AtlasY + textureDimension);
                var texcoord4 = new OpenTK.Vector2(glyph.AtlasX, glyph.AtlasY + textureDimension);

                vertexList.Add(new PT_VertexAttribute(charvertex1, texcoord1));
                vertexList.Add(new PT_VertexAttribute(charvertex2, texcoord2));
                vertexList.Add(new PT_VertexAttribute(charvertex3, texcoord3));
                vertexList.Add(new PT_VertexAttribute(charvertex4, texcoord4));

                index++;
                previous = ch;
            }

            vb = new DynamicVertexBuffer<PT_VertexAttribute>();
            var vertexArray = vertexList.ToArray();
            vb.BufferData<PT_VertexAttribute>(ref vertexArray);

            var v1 = new OpenTK.Vector3(left, top, 0);
            var v2 = new OpenTK.Vector3(right, top, 0);
            var v3 = new OpenTK.Vector3(right, bottom, 0);
            var v4 = new OpenTK.Vector3(left, bottom, 0);

            boxVertexList.Add(new PT_VertexAttribute(v1, new Vector2(0,0)));
            boxVertexList.Add(new PT_VertexAttribute(v2, new Vector2(1,0)));
            boxVertexList.Add(new PT_VertexAttribute(v3, new Vector2(1,1)));
            boxVertexList.Add(new PT_VertexAttribute(v4, new Vector2(0,1)));

            var boxVertexArray = boxVertexList.ToArray();
            boxVB = new DynamicVertexBuffer<PT_VertexAttribute>();
            boxVB.BufferData<PT_VertexAttribute>(ref boxVertexArray);
        }

        public string TextContent = "";

        private int fontSize = 36;

        private DynamicVertexBuffer<PT_VertexAttribute> vb = null;
        private DynamicVertexBuffer<PT_VertexAttribute> boxVB = null;

        private List<PT_VertexAttribute> vertexList = new List<PT_VertexAttribute>();
        private List<PT_VertexAttribute> boxVertexList = new List<PT_VertexAttribute>();

        private float left = float.MaxValue;
        private float right = float.MinValue;
        private float top = float.MinValue;
        private float bottom = float.MaxValue;
    }
}
