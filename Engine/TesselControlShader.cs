﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Core.OpenGLShader
{
    public class TesselControlShader  : Shader
    {
        public TesselControlShader()
        {
            ShaderObject = GL.CreateShader(ShaderType.TessControlShader);
        }
    }
}
