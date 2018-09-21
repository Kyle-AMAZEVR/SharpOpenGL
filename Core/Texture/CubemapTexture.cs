﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using FreeImageAPI;

namespace Core.Texture
{
    public class CubemapTexture : TextureBase
    {
        public CubemapTexture()
        {
            
        }

        public override void Bind()
        {
            if (IsValid)
            {
                GL.BindTexture(TextureTarget.TextureCubeMap,textureObject);
            }
        }

        
        
        public void Load()
        {
            Bind();

            string [] textureList = new string[]
            {
                PositiveX, NegativeX, PositiveY, NegativeY, PositiveZ, NegativeZ
            };

            for (int i = 0; i < textureList.Length; ++i)
            {
                using (var texture = new ScopedFreeImage(textureList[i]))
                {
                    GL.TexSubImage2D(TextureTarget.TextureCubeMapPositiveX, 0, 0, 0,
                        texture.Width, texture.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, ref texture.Bytes);
                }
            }
        }

        protected string NegativeX = "./Resources/Cubemap/uffizi-x.jpg";
        protected string PositiveX = "./Resources/Cubemap/uffizi+x.jpg";
        protected string NegativeY = "./Resources/Cubemap/uffizi-y.jpg";
        protected string PositiveY = "./Resources/Cubemap/uffizi+y.jpg";
        protected string NegativeZ = "./Resources/Cubemap/uffizi-z.jpg";
        protected string PositiveZ = "./Resources/Cubemap/uffizi+x.jpg";
    }
}