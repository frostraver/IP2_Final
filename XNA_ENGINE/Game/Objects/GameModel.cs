﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XNA_ENGINE.Engine;
using XNA_ENGINE.Engine.Scenegraph;
using XNA_ENGINE.Game.Scenes;

namespace XNA_ENGINE.Game.Objects
{
    class GameModel : GameObject3D
    {
        private readonly string _assetFile;
        private Model _model { get; set; }
        private bool m_UseTexture { get; set; }
        private Texture2D m_Texture;


        public GameModel(string assetFile)
        {
            _assetFile = assetFile;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _model = contentManager.Load<Model>(_assetFile);
            base.LoadContent(contentManager);
            
            /*foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = part.Effect.Clone();
                }
            }*/
        }

        public override void Draw(RenderContext renderContext)
        {
            var transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = renderContext.Camera.View;
                    effect.Projection = renderContext.Camera.Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * WorldMatrix;

                    if (m_UseTexture)
                    {
                        effect.DiffuseColor = new Vector3(1, 1, 1);
                        effect.TextureEnabled = true;
                        effect.Texture = m_Texture;
                    }
                    else
                    {
                        effect.DiffuseColor = new Vector3(0.345098f,0.694118f,0.105882f);
                        effect.TextureEnabled = false;
                    }
                }

                mesh.Draw();
            }

            base.Draw(renderContext);
        }

        public Model Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }

        public bool UseTexture
        {
            get
            {
                return m_UseTexture;
            }

            set
            {
                m_UseTexture = value;
            }
        }

        public void SetTexture(Texture2D texture)
        {
            m_UseTexture = true;
            m_Texture = texture;
        }
    }
}
