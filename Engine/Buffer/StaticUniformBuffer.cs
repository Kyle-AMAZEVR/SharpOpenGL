﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Core.Buffer
{
    public class StaticUniformBuffer : Core.Buffer.OpenGLBuffer
    {
        public StaticUniformBuffer()
        {
            mBufferTarget = BufferTarget.UniformBuffer;
            mHint = BufferUsageHint.StaticDraw;
        }
    }
}
