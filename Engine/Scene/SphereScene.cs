﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Primitive;
using OpenTK.Mathematics;
using Engine;
using Engine.Scene;

namespace Engine
{
    public class SphereScene : SceneBase
    {
        public override void InitializeScene()
        {
            var rusted = CreateGameObject<PBRSphere>();
            rusted.Scale = 1.5f;
            rusted.Translation = new Vector3(0, 0, -80);
            rusted.SetNormalTex("./Resources/Texture/rustediron/rustediron2_normal.dds");
            rusted.SetDiffuseTex("./Resources/Texture/rustediron/rustediron2_basecolor.dds");
            rusted.SetMetallicTex("./Resources/Texture/rustediron/rustediron2_metallic.dds");
            rusted.SetRoughnessTex("./Resources/Texture/rustediron/rustediron2_roughness.dds");

            var gold = CreateGameObject<PBRSphere>();
            gold.Scale = 1.5f;
            gold.Translation = new Vector3(0, 0, -40);
            gold.SetNormalTex("./Resources/Texture/gold/gold-scuffed_normal.dds");
            gold.SetDiffuseTex("./Resources/Texture/gold/gold-scuffed_basecolor.dds");
            gold.SetMetallicTex("./Resources/Texture/gold/gold-scuffed_metallic.dds");
            gold.SetRoughnessTex("./Resources/Texture/gold/gold-scuffed_roughness.dds");

            var wood = CreateGameObject<PBRSphere>();
            wood.Scale = 1.5f;
            wood.Translation = new Vector3(-0, 0, 0);
            wood.SetNormalTex("./Resources/Texture/floor/mahogfloor_normal.dds");
            wood.SetDiffuseTex("./Resources/Texture/floor/mahogfloor_basecolor.dds");
            wood.SetRoughnessTex("./Resources/Texture/floor/mahogfloor_roughness.dds");

            var bamboo = CreateGameObject<PBRSphere>();
            bamboo.Scale = 1.5f;
            bamboo.Translation = new Vector3(0, 0, 40);
            bamboo.SetNormalTex("./Resources/Texture/bamboo/bamboo-wood-semigloss-normal.dds");
            bamboo.SetDiffuseTex("./Resources/Texture/bamboo/bamboo-wood-semigloss-albedo.dds");
            bamboo.SetRoughnessTex("./Resources/Texture/bamboo/bamboo-wood-semigloss-roughness.dds");
            bamboo.SetMetallicTex("./Resources/Texture/bamboo/bamboo-wood-semigloss-metal.dds");

            var metal = CreateGameObject<PBRSphere>();
            metal.Scale = 1.5f;
            metal.Translation = new Vector3(0, 0, 80);
            metal.SetNormalTex("./Resources/Texture/metal/metal-splotchy-normal-dx.dds");
            metal.SetDiffuseTex("./Resources/Texture/metal/metal-splotchy-albedo.dds");
            metal.SetRoughnessTex("./Resources/Texture/metal/metal-splotchy-rough.dds");
            metal.SetMetallicTex("./Resources/Texture/metal/metal-splotchy-metal.dds");

            var tile = CreateGameObject<PBRSphere>();
            tile.Scale = 1.5f;
            tile.Translation = new Vector3(0, 0, 120);
            tile.SetNormalTex("./Resources/Texture/tile/Tiles32_nrm.dds");
            tile.SetDiffuseTex("./Resources/Texture/tile/Tiles32_col.dds");
            tile.SetRoughnessTex("./Resources/Texture/tile/Tiles32_rgh.dds");

            InitializeCamera();
        }

        public override void Render()
        {
            foreach (var item in mGameObjectList)
            {
                item.Render();
            }
        }

        protected override SceneRendererBase CreateSceneRenderer()
        {
            return new DefaultSceneRenderer();
        }
    }
}