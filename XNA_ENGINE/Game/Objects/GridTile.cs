﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XNA_ENGINE.Engine;
using XNA_ENGINE.Engine.Objects;
using XNA_ENGINE.Engine.Scenegraph;
using XNA_ENGINE.Game.Objects;


namespace XNA_ENGINE.Game.Objects
{
    public class GridTile
    {
        private GameModel m_TileModel;
        private int m_Row, m_Column;

        private static float GRIDWIDTH = 65;
        private static float GRIDDEPTH = 65;
      //  private static float GRIDHEIGHT = 32;

        private string m_TileType;
        private string m_TileSettlement;

        private bool m_Selected { get; set; }

        public GridTile(GameScene pGameScene, int row, int column)
        {
            m_Row = row;
            m_Column = column;

            m_TileModel = new GameModel("Models/tile_Template");
            m_TileModel.Translate(new Vector3(GRIDWIDTH * m_Row, 0, GRIDDEPTH * m_Column));
            pGameScene.AddSceneObject(m_TileModel);

            m_Selected = false;
        }

        public void Initialize()
        {
            m_TileType = "normal";
        }

        public void Update(Engine.RenderContext renderContext)
        {
            if (m_Selected)
                m_TileModel.Rotate(0, 45.0f * (float)renderContext.GameTime.TotalGameTime.TotalSeconds, 0);
        }

        public void SetTileType(string type)
        {
            m_TileType = type;
        }

        public string GetTileType()
        {
            return m_TileType;
        }

        public void SetTileSettlement(string type)
        {
            m_TileSettlement = type;
        }

        public string GetTileSettlement()
        {
            return m_TileSettlement;
        }
    }
}