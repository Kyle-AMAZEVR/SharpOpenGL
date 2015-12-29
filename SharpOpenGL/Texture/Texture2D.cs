﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using SharpOpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;
using System.Drawing.Imaging;

namespace SharpOpenGL.Texture
{
    public class Texture2D : IDisposable
    {
        public Texture2D()
        {
            m_TextureObject = GL.GenTexture();
        }

        public void Dispose()
        {
            if(m_TextureObject != -1)
            {
                GL.DeleteTexture(m_TextureObject);
                m_TextureObject = -1;                
            }
        }

        public void Bind()
        {
            if(m_TextureObject != -1)
            {
                GL.BindTexture(TextureTarget.Texture2D, m_TextureObject);
            }
        }

        public void LoadBitmap(string FilePath)
        {
            using(var bitmap = new Bitmap(FilePath))
            {
                var bmpData = bitmap.LockBits(new Rectangle(0,0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, bmpData.Scan0);
            }            
        }


        
        public int Width { get { return m_Width; } }
        public int Height { get { return m_Height; } }
        
        protected int m_Width = 0;
        protected int m_Height = 0;

        int m_TextureObject = -1;
    }
}
