﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GLTF.V1;
using GLTF.V2;
using System.IO;

namespace GLTF
{
    public class GLTFLoader
    {
        public static GLTF_V2 LoadGLTFV2(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var result = JsonSerializer.Deserialize<GLTF_V2>(json);
                return result;
            }
            catch (Exception e)
            {
                //
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static async Task<GLTF_V2> LoadGLTF2Async(string path)
        {
            try
            {
                var json = await File.ReadAllBytesAsync(path);
                using (var m = new MemoryStream(json))
                {
                    GLTF_V2 result = await JsonSerializer.DeserializeAsync<GLTF_V2>(m);
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}
