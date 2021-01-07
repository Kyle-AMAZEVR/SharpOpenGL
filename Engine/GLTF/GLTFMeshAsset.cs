﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GLTF.V2;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows.Documents.DocumentStructures;
using System.Xaml;
using Core;
using Core.Primitive;
using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using AttributeType = GLTF.V2.AttributeType;

namespace GLTF
{
    public enum PBRTextureType
    {
        Emissive,
        Normal,
        Occlusion,
        BaseColor,
        MetallicRoughness,
    }

    public class PBRInfo
    {
        public string EmissiveTexture;
        public string NormalTexture;
        public string OcclusionTexture;
        public string BaseColorTexture;
    }

    public class VertexAttributeSemantic : IEquatable<VertexAttributeSemantic>
    {
        public readonly int index = 0;
        public readonly string name = "";
        public readonly AttributeType attributeType = AttributeType.SCALAR;


        public VertexAttributeSemantic(int attributeIndex, string attributeName, AttributeType @type)
        {
            this.index = attributeIndex;
            this.name = attributeName;
            this.attributeType = @type;
        }
        public bool Equals(VertexAttributeSemantic semantic)
        {
            return index == semantic.index && name == semantic.name;
        }

        public bool Equals(object o)
        {
            return Equals(o as VertexAttributeSemantic);
        }
        
        public override int GetHashCode()
        {
            return index;
        }
    }
    public class GLTFMeshAsset
    {
        public static List<GLTFMeshAsset> LoadFrom(GLTF_V2 gltf)
        {
            // buffer
            List<byte[]> bufferDatas = new List<byte[]>();
            List<byte[]> bufferViews = new List<byte[]>();
            Dictionary<int,AttributeType> bufferViewAttributeTypes = new Dictionary<int, AttributeType>();
            Dictionary<int, ComponentType> bufferViewComponentTypes = new Dictionary<int, ComponentType>();

            // bufferView
            Dictionary<int, List<Vector4>> vector4BufferViews = new Dictionary<int, List<Vector4>>();
            Dictionary<int, List<Vector3>> vector3BufferViews = new Dictionary<int, List<Vector3>>();
            Dictionary<int, List<Vector2>> vector2BufferViews = new Dictionary<int, List<Vector2>>();

            Dictionary<int, List<ushort>> uShortBufferViews = new Dictionary<int, List<ushort>>();
            Dictionary<int, List<uint>> uIntBufferViews = new Dictionary<int, List<uint>>();
            Dictionary<int, List<float>> floatBufferViews = new Dictionary<int, List<float>>();

            var baseDir = Path.GetDirectoryName(gltf.Path);

            // buffers
            for (int i = 0; i < gltf.buffers.Count; ++i)
            {
                var filepath = gltf.buffers[i].uri;

                Debug.Assert(File.Exists(Path.Combine(baseDir, filepath)));

                byte[] result = File.ReadAllBytes(Path.Combine(baseDir, filepath));
                bufferDatas.Add(result);
            }

            // guess attribute & component type
            for (int i = 0; i < gltf.accessors.Count; ++i)
            {
                if (bufferViewComponentTypes.ContainsKey(gltf.accessors[i].bufferView) == false)
                {
                    bufferViewComponentTypes.Add(gltf.accessors[i].bufferView, gltf.accessors[i].componentType);
                }

                if (bufferViewAttributeTypes.ContainsKey(gltf.accessors[i].bufferView) == false)
                {
                    bufferViewAttributeTypes.Add(gltf.accessors[i].bufferView, gltf.accessors[i].type);
                }
            }

            // buffer views
            for (int bufferViewIndex = 0; bufferViewIndex < gltf.bufferViews.Count; ++bufferViewIndex)
            {
                int bufferIndex = gltf.bufferViews[bufferViewIndex].buffer;

                var span = new Span<byte>(bufferDatas[bufferIndex], gltf.bufferViews[bufferViewIndex].byteOffset, gltf.bufferViews[bufferViewIndex].byteLength);

                Debug.Assert(bufferViewComponentTypes.ContainsKey(bufferViewIndex));
                Debug.Assert(bufferViewAttributeTypes.ContainsKey(bufferViewIndex));

                var attributeType = bufferViewAttributeTypes[bufferViewIndex];
                var componentType = bufferViewComponentTypes[bufferViewIndex];

                int countPerRead = 1;
                int bytesPerRead = 1;

                switch (attributeType)
                {
                    case AttributeType.VEC3:
                        countPerRead = 3;
                        break;
                    case AttributeType.VEC2:
                        countPerRead = 2;
                        break;
                    case AttributeType.VEC4:
                        countPerRead = 4;
                        break;
                    case AttributeType.SCALAR:
                        countPerRead = 1;
                        break;
                    case AttributeType.MAT2:
                        countPerRead = 4;
                        break;
                    case AttributeType.MAT3:
                        countPerRead = 9;
                        break;
                }

                switch (componentType)
                {
                    case ComponentType.UNSIGNED_INT:
                    case ComponentType.FLOAT:
                        bytesPerRead = 4;
                        break;
                    case ComponentType.BYTE:
                    case ComponentType.UNSIGNED_BYTE:
                        bytesPerRead = 1;
                        break;
                    case ComponentType.SHORT:
                    case ComponentType.UNSIGNED_SHORT:
                        bytesPerRead = 2;
                        break;
                }

                // convert bytes to specific type array
                int stride = countPerRead * bytesPerRead;

                int readcount = gltf.bufferViews[bufferViewIndex].byteLength / stride;

                if (attributeType == AttributeType.VEC3)
                {
                    Debug.Assert(vector3BufferViews.ContainsKey(bufferViewIndex) == false);
                    vector3BufferViews.Add(bufferViewIndex, new List<Vector3>());
                }
                else if (attributeType == AttributeType.VEC2)
                {
                    Debug.Assert(vector2BufferViews.ContainsKey(bufferViewIndex) == false);
                    vector2BufferViews.Add(bufferViewIndex, new List<Vector2>());
                }
                else if (attributeType == AttributeType.VEC4)
                {
                    Debug.Assert(vector4BufferViews.ContainsKey(bufferViewIndex) == false);
                    vector4BufferViews.Add(bufferViewIndex, new List<Vector4>());
                }
                else if (attributeType == AttributeType.SCALAR)
                {
                    if (componentType == ComponentType.UNSIGNED_SHORT)
                    {
                        uShortBufferViews.Add(bufferViewIndex, new List<ushort>());
                    }
                    else if (componentType == ComponentType.UNSIGNED_INT)
                    {
                        uIntBufferViews.Add(bufferViewIndex, new List<uint>());
                    }
                }

                for (int i = 0; i < readcount; ++i)
                {
                    var sliced = span.Slice(i * stride, stride);
                    if (attributeType == AttributeType.VEC3)
                    {
                        var parsed = ToVector3(ref sliced);
                        vector3BufferViews[bufferViewIndex].Add(parsed);
                    }
                    else if (attributeType == AttributeType.VEC2)
                    {
                        var parsed = ToVector2(ref sliced);
                        vector2BufferViews[bufferViewIndex].Add(parsed);
                    }
                    else if (attributeType == AttributeType.VEC4)
                    {
                        var parsed = ToVector4(ref sliced);
                        vector4BufferViews[bufferViewIndex].Add(parsed);
                    }
                    else if (attributeType == AttributeType.SCALAR)
                    {
                        if (componentType == ComponentType.UNSIGNED_SHORT)
                        {
                            var parsed = ToUShort(ref sliced);
                            uShortBufferViews[bufferViewIndex].Add(parsed);
                        }
                        else if (componentType == ComponentType.UNSIGNED_INT)
                        {
                            var parsed = ToUInt(ref sliced);
                            uIntBufferViews[bufferViewIndex].Add(parsed);
                        }
                        else if (componentType == ComponentType.FLOAT)
                        {
                            var parsed = BitConverter.ToSingle(sliced);
                            floatBufferViews[bufferViewIndex].Add(parsed);
                        }
                    }
                }
            }

            List<GLTFMeshAsset> parsedMeshList = new List<GLTFMeshAsset>();

            for (int i = 0; i < gltf.meshes.Count; ++i)
            {
                var mesh = new GLTFMeshAsset();

                // for each mesh
                // parser index array and vertex attributes
                for (int pindex = 0; pindex < gltf.meshes[i].primitives.Count; ++pindex)
                {
                    // index array
                    int indexArraryAccessorIndex = gltf.meshes[i].primitives[pindex].indices;
                    int indexArrayBufferViewIndex = gltf.accessors[indexArraryAccessorIndex].bufferView;

                    if (gltf.accessors[indexArraryAccessorIndex].componentType == ComponentType.UNSIGNED_INT)
                    {
                        mesh.mUIntIndices = uIntBufferViews[indexArrayBufferViewIndex];
                    }
                    else if (gltf.accessors[indexArraryAccessorIndex].componentType == ComponentType.UNSIGNED_SHORT)
                    {
                        mesh.mUShortIndices = uShortBufferViews[indexArrayBufferViewIndex];
                    }

                    // vertex attributes
                    foreach (var kvp in gltf.meshes[i].primitives[pindex].attributes)
                    {
                        string semantic = kvp.Key;
                        int accessorIndex = kvp.Value;
                        int bufferViewIndex = gltf.accessors[accessorIndex].bufferView;

                        var attributeSemantic = new VertexAttributeSemantic(bufferViewIndex, semantic,
                            gltf.accessors[accessorIndex].type);

                        // we found new vertex attribute
                        if (!mesh.mVertexAttributeMap.ContainsKey(semantic))
                        {
                            mesh.mVertexAttributeMap.Add(semantic, attributeSemantic);

                            if (gltf.accessors[accessorIndex].type == AttributeType.SCALAR)
                            {
                                mesh.mFloatVertexAttributes.Add(attributeSemantic,
                                    floatBufferViews[bufferViewIndex]);
                            }
                            else if (gltf.accessors[accessorIndex].type == AttributeType.VEC2)
                            {
                                mesh.mVector2VertexAttributes.Add(attributeSemantic,
                                    vector2BufferViews[bufferViewIndex]);
                            }
                            else if (gltf.accessors[accessorIndex].type == AttributeType.VEC3)
                            {
                                mesh.mVector3VertexAttributes.Add(attributeSemantic,
                                    vector3BufferViews[bufferViewIndex]);
                            }
                            else if (gltf.accessors[accessorIndex].type == AttributeType.VEC4)
                            {
                                mesh.mVector4VertexAttributes.Add(attributeSemantic,
                                    vector4BufferViews[bufferViewIndex]);
                            }
                        }
                    }
                }
                

                parsedMeshList.Add(mesh);
            }

            return parsedMeshList;
        }

        public GLTFMeshAsset()
        {
        }

        public PBRInfo PBRInfo = new PBRInfo();

        public List<string> TexturePaths { get; set; }

        public string Name { get; set; }

        public List<VertexAttributeSemantic> VertexAttributeList
        {
            get => mVertexAttributeList;
        }

        public Dictionary<string, VertexAttributeSemantic> VertexAttributeMap
        {
            get => mVertexAttributeMap;
        }

        public Dictionary<VertexAttributeSemantic, List<float>> FloatVertexAttributes
        {
            get => mFloatVertexAttributes;
        }
        public Dictionary<VertexAttributeSemantic, List<Vector2>> Vector2VertexAttributes
        {
            get => mVector2VertexAttributes;
        }
        public Dictionary<VertexAttributeSemantic, List<Vector3>> Vector3VertexAttributes
        {
            get => mVector3VertexAttributes;
        }
        public Dictionary<VertexAttributeSemantic, List<Vector4>> Vector4VertexAttributes
        {
            get => mVector4VertexAttributes;
        }

        public List<uint> UIntIndices
        {
            get => mUIntIndices;
        }
        public List<ushort> UShortIndices
        {
            get => mUShortIndices;
        }

        protected List<VertexAttributeSemantic> mVertexAttributeList = new List<VertexAttributeSemantic>();

        protected Dictionary<string, VertexAttributeSemantic> mVertexAttributeMap = new Dictionary<string, VertexAttributeSemantic>();

        protected Dictionary<VertexAttributeSemantic, List<float>> mFloatVertexAttributes = new Dictionary<VertexAttributeSemantic, List<float>>();

        protected Dictionary<VertexAttributeSemantic, List<Vector3>> mVector3VertexAttributes = new Dictionary<VertexAttributeSemantic, List<Vector3>>();

        protected Dictionary<VertexAttributeSemantic, List<Vector2>> mVector2VertexAttributes = new Dictionary<VertexAttributeSemantic, List<Vector2>>();

        protected Dictionary<VertexAttributeSemantic, List<Vector4>> mVector4VertexAttributes = new Dictionary<VertexAttributeSemantic, List<Vector4>>();

        protected List<uint> mUIntIndices= new List<uint>();

        protected List<ushort> mUShortIndices = new List<ushort>();

        protected List<byte[]> mBufferViews = new List<byte[]>();

        private static uint ToUInt(ref Span<byte> span)
        {
            return BitConverter.ToUInt32(span);
        }

        private static ushort ToUShort(ref Span<byte> span)
        {
            return BitConverter.ToUInt16(span);
        }

        private static Vector3 ToVector3(ref Span<byte> span)
        {
            var xPart=span.Slice(0, 4);
            var yPart=span.Slice(4, 4);
            var zPart =span.Slice(8, 4);
            var x = BitConverter.ToSingle(xPart);
            var y = BitConverter.ToSingle(yPart);
            var z = BitConverter.ToSingle(zPart);

            return new Vector3(x,y,z);
        }

        private static Vector2 ToVector2(ref Span<byte> span)
        {
            var xPart = span.Slice(0, 4);
            var yPart = span.Slice(4, 4);
            
            var x = BitConverter.ToSingle(xPart);
            var y = BitConverter.ToSingle(yPart);
            

            return new Vector2(x, y);
        }

        private static Vector4 ToVector4(ref Span<byte> span)
        {
            var xPart = span.Slice(0, 4);
            var yPart = span.Slice(4, 4);
            var zPart = span.Slice(8, 4);
            var wPart = span.Slice(12, 4);

            var x = BitConverter.ToSingle(xPart);
            var y = BitConverter.ToSingle(yPart);
            var z = BitConverter.ToSingle(zPart);
            var w = BitConverter.ToSingle(wPart);

            return new Vector4(x, y, z,w);
        }
    }
}