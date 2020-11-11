﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using Core;

namespace SharpOpenGL
{
    public class MainThreadQueue : Singleton<MainThreadQueue>
    {
        public void Enqueue(Action action)
        {
            Queue.Enqueue(action);
        }

        public void Execute()
        {
            Debug.Assert(Thread.CurrentThread.ManagedThreadId == OpenGLContext.Get().MainTheadId);

            while (!Queue.IsEmpty)
            {
                Action action;
                Queue.TryDequeue(out action);
                action();
            }
        }

        protected ConcurrentQueue<Action> Queue = new ConcurrentQueue<Action>();
    }
}
