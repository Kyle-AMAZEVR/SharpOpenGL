﻿using System;
using System.Collections.Generic;
using System.Text;
using Engine.Scene;

namespace Engine
{
    public class SceneRendererBase 
    {
        public SceneRendererBase()
        {

        }

        public virtual void Initialize()
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