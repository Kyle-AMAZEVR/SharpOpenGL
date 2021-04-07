﻿using Core.Buffer;
using Core.OpenGLShader;
using Core.Texture;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Core.CustomAttribute;
using Engine;


namespace Core.MaterialBase
{
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct CameraTransform
    {
        [FieldOffset(0), ExposeUI]
        public OpenTK.Mathematics.Matrix4 View;
        [FieldOffset(64), ExposeUI]
        public OpenTK.Mathematics.Matrix4 Proj;
    }

    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct ModelTransform
    {
        [FieldOffset(0), ExposeUI]
        public OpenTK.Mathematics.Matrix4 Model;
    }

    public class MaterialBase : Core.AssetBase, IBindable
    {
        protected ShaderProgram mMaterialProgram = null;
        protected Core.OpenGLShader.VertexShader mVertexShader = null;
        protected Core.OpenGLShader.FragmentShader mFragmentShader = null;

        protected string VertexShaderCode = "";
        protected string FragmentShaderCode = "";

        public static readonly string StageOutputName = "stage_out";
        public static readonly string StageInputName = "stage_in";

        protected string CompileResult = "";
        
        public MaterialBase(ShaderProgram program)
        {
            mMaterialProgram = program;
            Initialize();
        }

        public MaterialBase(string vertexShaderCode, string fragmentShaderCode, 
            List<Tuple<string, string>> vsDefines, List<Tuple<string, string>> fsDefines) 
        {
            mVertexShader = new VertexShader();
            mFragmentShader = new FragmentShader();

            mMaterialProgram = new Core.OpenGLShader.ShaderProgram();

            mVertexShader.CompileShader(vertexShaderCode, vsDefines);
            mFragmentShader.CompileShader(fragmentShaderCode, fsDefines);

            mMaterialProgram.AttachShader(mVertexShader);
            mMaterialProgram.AttachShader(mFragmentShader);

            bool bSuccess = mMaterialProgram.LinkProgram(out CompileResult);
            if (bSuccess == false)
            {
                Console.Write("{0}", CompileResult);
            }
            Debug.Assert(bSuccess == true);

            Initialize();
        }

        public MaterialBase(string vertexShaderCode, string fragmentShaderCode)
        {
            mVertexShader = new VertexShader();
            mFragmentShader = new FragmentShader();

            mMaterialProgram = new Core.OpenGLShader.ShaderProgram();
            
            mVertexShader.CompileShader(vertexShaderCode);
            mFragmentShader.CompileShader(fragmentShaderCode);

            mMaterialProgram.AttachShader(mVertexShader);
            mMaterialProgram.AttachShader(mFragmentShader);

            bool bSuccess = mMaterialProgram.LinkProgram(out CompileResult);

            if (bSuccess == false)
            {
                Console.Write("{0}", CompileResult);
            }

            Debug.Assert(bSuccess == true);

            Initialize();
        }

        public MaterialBase()
        {
        }

        protected virtual void CleanUpUniformBufferMap()
        {
            if(mUniformBufferMap != null)
            {
                foreach(var buffer in mUniformBufferMap)
                {
                    buffer.Value.Dispose();
                }
                mUniformBufferMap.Clear();
            }
        }

        protected virtual void BuildUniformBufferMap()
        {
            var names = mMaterialProgram.GetActiveUniformBlockNames();

            foreach (var name in names)
            {
                var uniformBuffer = new DynamicUniformBuffer(mMaterialProgram, name);
                mUniformBufferMap.Add(name, uniformBuffer);
            }
        }

        protected virtual void BuildSamplerMap()
        {
            var samplerNames = mMaterialProgram.GetSampler2DNames();

            for (int i = 0; i < samplerNames.Count; ++i)
            {
                int index = mMaterialProgram.GetSampler2DUniformLocation(samplerNames[i]);
                mSamplerMap.Add(samplerNames[i], index);
            }
        }


        protected virtual void Initialize()
        {
            Debug.Assert(RenderingThread.IsInRenderingThread());

            BuildUniformBufferMap();

            BuildSamplerMap();

            mUniformVariableNames = mMaterialProgram.GetActiveUniformNames();

            foreach (var name in mUniformVariableNames)
            {
                var loc= mMaterialProgram.GetUniformLocation(name);
                mUniformVariableMap.Add(name, loc);
            }
        }

        public void SetTexture(int location, Core.Texture.TextureBase texture)
        {
            SetTextureByIndex(location, texture, Sampler.DefaultLinearSampler);
        }

        public void SetTexture(string name, Core.Texture.TextureBase texture)
        {
            SetTexture(name, texture, Sampler.DefaultLinearSampler);
        }

        public void SetTexture(string name, Core.Texture.TextureBase texture, Sampler sampler)
        {
            if (mSamplerMap == null)
            {
                return;
            }

            if (mSamplerMap.ContainsKey(name) == false)
            {
                return;
            }

            var location = mSamplerMap[name];
            SetTextureByIndex(location, texture, sampler);
        }

        // set texture by index
        public void SetTextureByIndex(int index, Core.Texture.TextureBase texture, Sampler sampler)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + index);
            texture.Bind();
            sampler.BindSampler(TextureUnit.Texture0 + index);
            texture.BindShader(TextureUnit.Texture0 + index, index);
        }

        public void Bind()
        {
            Setup();
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public virtual void Setup()
        {
            mMaterialProgram.UseProgram();

            if (mUniformBufferMap != null)
            {
                foreach (var uniformBuffer in mUniformBufferMap)
                {
                    mMaterialProgram.BindUniformBlock(uniformBuffer.Key);
                }
            }
        }

        public string GetCompileResult()
        {
            return CompileResult;
        }

        public bool HasUniformBuffer(string uniformBufferName)
        {
            if(mUniformBufferMap.ContainsKey(uniformBufferName))
            {
                return true;
            }

            return false;
        }
        
        public void SetUniformBufferValue<T>(string bufferName, ref T data) where T : struct
        {
            if(HasUniformBuffer(bufferName))
            {   
                mUniformBufferMap[bufferName].BufferData(ref data);
            }
        }


        public void SetUniformBufferMemberValue<TMember>(string bufferName, ref TMember data, int offset) where TMember : struct
        {
            if (HasUniformBuffer(bufferName))
            {   
                mUniformBufferMap[bufferName].BufferSubData<TMember>(ref data, offset);
            }
        }

        public void SetUniformVarData(string varName, float data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, bool data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, int data, bool bChecked=false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, float[] data, bool bChecked = false)
        {
            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformFloatArrayData(varName , ref data);
            }
        }

        public void SetUniformVarData(string varName, ref float[] data, bool bChecked = false)
        {
            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformFloatArrayData(varName, ref data);
            }
        }

        public void SetUniformVarData(string varName, double[] data, bool bChecked = false)
        {
            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformDoubleArrayData(varName, ref data);
            }
        }

        public void SetUniformVarData(string varname, ref Vector2[] data)
        {
            if (mUniformVariableNames.Contains(varname))
            {
                mMaterialProgram.SetUniformVector2ArrayData(varname, ref data);
            }
        }

        public void SetUniformVarData(string varname, ref Vector3[] data)
        {
            if (mUniformVariableNames.Contains(varname))
            {
                mMaterialProgram.SetUniformVector3ArrayData(varname, ref data);
            }
        }

        public void SetUniformVarData(string varname, ref Vector4[] data)
        {
            if (mUniformVariableNames.Contains(varname))
            {
                mMaterialProgram.SetUniformVector4ArrayData(varname, ref data);
            }
        }

        public void SetUniformVarData(string varName, Vector2 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, ref Vector2 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }        

        public void SetUniformVarData(string varName, Vector3 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, ref Vector3 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, ref data);
            }
        }

        public void SetUniformVarData(string varName, Vector4 data, bool bChecked=false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, Matrix3 data, bool bChecked=false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        private void CheckUniformVariableExist(string variablename)
        {
            Debug.Assert(mUniformVariableNames.Contains(variablename));
        }

        public void SetUniformVarData(string varName, Matrix4 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, data);
            }
        }

        public void SetUniformVarData(string varName, ref Matrix4 data, bool bChecked = false)
        {
            if (bChecked)
            {
                CheckUniformVariableExist(varName);
            }

            if (mUniformVariableNames.Contains(varName))
            {
                mMaterialProgram.SetUniformVarData(varName, ref data);
            }
        }

        public void SetUniformVector2ArrayData(string varName, ref float[] data)
        {
            Debug.Assert(mUniformVariableNames.Contains(varName));
            mMaterialProgram.SetUniformVector2ArrayData(varName, ref data);
        }

        public void SetUniformVector3ArrayData(string varName, ref float[] data)
        {
            Debug.Assert(mUniformVariableNames.Contains(varName));
            mMaterialProgram.SetUniformVector3ArrayData(varName, ref data);
        }

        public void SetUniformVector4ArrayData(string varName, ref float[] data)
        {
            Debug.Assert(mUniformVariableNames.Contains(varName));
            mMaterialProgram.SetUniformVector4ArrayData(varName, ref data);
        }

        public List<VertexAttribute> GetVertexAttributes()
        {
            if (mMaterialProgram.ProgramLinked)
            {
                return mMaterialProgram.GetActiveVertexAttributeList();
            }

            return new List<VertexAttribute>();
        }

        public List<ActiveAttribType> GetVertexAttributeTypeList()
        {
            if (mMaterialProgram.ProgramLinked)
            {
                return mMaterialProgram.GetActiveVertexAttributeList().Select(x=>x.AttributeType).ToList();
            }
            return new List<ActiveAttribType>();
        }

        protected CameraTransform mCameraTransform = new CameraTransform();
        protected ModelTransform mModelTransform = new ModelTransform();

        protected Dictionary<string, DynamicUniformBuffer> mUniformBufferMap = new Dictionary<string, DynamicUniformBuffer>();
        protected Dictionary<string, int> mUniformVariableMap = new Dictionary<string, int>();
        protected Dictionary<string, int> mSamplerMap = new Dictionary<string, int>();
        

        protected List<string> mUniformVariableNames = null;
    }
}
