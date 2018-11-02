﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.Shapes;
using Core;
using Core.Buffer;
using Core.MaterialBase;
using Core.Primitive;
using Core.Texture;
using FreeTypeLibWrapper;
using OpenTK.Graphics.OpenGL;
using SharpOpenGL.Asset;
using SixLabors.Primitives;
using System.Diagnostics;



namespace SharpOpenGL.Font
{
    public class FontManager : Singleton<FontManager>
    {
        public void Initialize()
        {
            fontAtlas = new Texture2D();
            FontRenderMaterial = AssetManager.LoadAssetSync<MaterialBase>("FontRenderMaterial");
            freeTypeLib = new FreeType();
            BuildFontTextureAtlas();
        }

        public void BuildFontTextureAtlas()
        {
            var characters = Enumerable.Range(char.MinValue, 126).Select(c => (char)c).Where(c => !char.IsControl(c)).ToArray();
            var charString = new string (characters);
            bool bSuccess = freeTypeLib.Initialize("./Resources/Font/Consola.ttf", 54, charString);
            if (bSuccess)
            {
                GlyphDictionary = freeTypeLib.GetGlyphInfoDictionary();
                realTextureSize = freeTypeLib.GetRealTextureSize();
                textureDimension = freeTypeLib.GetTextureDimension();
                squareSize = freeTypeLib.GetSquareSize();
            }
            else
            {
                Debug.Assert(false);
            }
            var textureData = freeTypeLib.GetTextureData();

            using(Image<Rgba32> img = new Image<Rgba32>(realTextureSize, realTextureSize))
            {
                img.Mutate(x => x.Fill(Rgba32.Black));
                for (int x = 0; x < realTextureSize; ++x)
                {
                    for (int y = 0; y < realTextureSize; ++y)
                    {
                        var index = (y * realTextureSize + x) * 2;
                        if (textureData[index] > 0)
                        {
                            Rgba32 color = new Rgba32();
                            color.R = textureData[index];
                            color.G = textureData[index];
                            color.B = textureData[index];
                            //color.A = textureData[index];
                            img[x, y] = color;
                        }
                    }
                }

                using (FileStream fs = File.Create("freetype.png"))
                {
                    img.SaveAsPng(fs);
                }
            }
            
            fontAtlas.Load(textureData.ToArray(), realTextureSize, realTextureSize, PixelInternalFormat.LuminanceAlpha, PixelFormat.LuminanceAlpha);
            
        }

        public void RenderText(float x, float y, string text, int fontSize)
        {
            if (renderTextDicationary.ContainsKey(text) == false)
            {
                var newInstance = new TextInstance(text,x,y,fontSize);
                renderTextDicationary.Add(text, newInstance);
            }
            
            renderTextDicationary[text].Render(FontRenderMaterial);
        }

        
        // 
        private Texture2D fontAtlas = null;
        private MaterialBase FontRenderMaterial = null;
        //

        public static Dictionary<ulong, FreeTypeLibWrapper.GlyphInfo> GlyphDictionary = null;

        public int FontSize => fontSize;

        

        // texture atlas info
        private float textureDimension = 0;
        private float squareSize = 0;
        
        private int fontSize = 18;
        private int realTextureSize = 512;
        private bool bInitialized = false;

        public float SquareSize => squareSize;
        public float TextureDimension => textureDimension;


        public Texture2D FontAtlas
        {
            get { return fontAtlas; }
        }

        Dictionary<string, TextInstance> renderTextDicationary = new Dictionary<string, TextInstance>();
        private FreeTypeLibWrapper.FreeType freeTypeLib = null;
    }
}
