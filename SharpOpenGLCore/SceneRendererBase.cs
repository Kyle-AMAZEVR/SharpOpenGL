﻿using System;
using System.Collections.Generic;
using System.Text;
using SharpOpenGL.Scene;

namespace SharpOpenGLCore
{
    public class SceneRendererBase 
    {
        public SceneRendererBase()
        {

        }

        public virtual void LoadScene()
        {

        }

        public virtual void RenderScene(SceneBase scene)
        {
            //
        }
        public virtual void UnloadScene()
        {

        }

        protected bool mSceneLoaded = false;
    }
}
