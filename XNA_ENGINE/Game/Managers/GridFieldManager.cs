﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA_ENGINE.Engine.Scenegraph;
using XNA_ENGINE.Game.Objects;

namespace XNA_ENGINE.Game.Managers
{
    class GridFieldManager
    {
        public enum SelectionMode
        {
            select1x1,
            select2x2,

            enumSize
        }

        //Singleton implementation
        private static GridFieldManager m_GridFieldManager;

        private GridTile[,] m_GridField;


        private const int GRID_ROW_LENGTH = 30;
        private const int GRID_COLUMN_LENGTH = 30;

        private SelectionMode m_SelectionMode = SelectionMode.select1x1;

        // private int GRID_OFFSET = 64;

        private Random m_Random;

        private GridFieldManager(GameScene pGameScene)
        {
            CreativeMode = false;

            // Load Map
            m_GridField = MapLoadSave.GetInstance().LoadMap(pGameScene, "GeneratedTileMap");
        }

        static public GridFieldManager GetInstance(GameScene pGameScene)
        {
            if (m_GridFieldManager == null)
                m_GridFieldManager = new GridFieldManager(pGameScene);

            return m_GridFieldManager;
        }

        public void Initialize()
        {
            m_Random = new Random();

            //Iterate over every GridTile
            for (int i = 0; i < GRID_ROW_LENGTH; ++i)
            {
                for (int j = 0; j < GRID_COLUMN_LENGTH; ++j)
                {
                    m_GridField[i, j].Initialize();
                }
            }
        }

        public void Update(Engine.RenderContext renderContext)
        {
            //Iterate over every GridTile
            for (int i = 0; i < GRID_ROW_LENGTH; ++i)
            {
                for (int j = 0; j < GRID_COLUMN_LENGTH; ++j)
                {
                    m_GridField[i,j].Update(renderContext);
                }
            }
        }

        public GridTile HitTestField(Ray ray)
        {
            //Iterate over every GridTile
            for (int i = 0; i < GRID_ROW_LENGTH; ++i)
            {
                for (int j = 0; j < GRID_COLUMN_LENGTH; ++j)
                {
                    if (m_GridField[i,j].HitTest(ray))
                        return m_GridField[i, j];
                }
            }
            return null;
        }

        public GridTile GetSelectedTile()
        {
            foreach (var gridTile in m_GridField)
            {
                if (gridTile.PermanentSelected)
                {
                    return gridTile;
                }
            }

            return null;
        }

        public void PermanentSelect(int row, int column)
        {
            bool value = m_GridField[row, column].PermanentSelected;
            foreach (var gridTile in m_GridField)
            {
                gridTile.PermanentSelected = false;
            }

            m_GridField[row, column].PermanentSelected = !value;
        }

        public SelectionMode SelectionModeMeth
        {
            get { return m_SelectionMode; }
            set { m_SelectionMode = value; }
        }

        public Random Random
        {
            get { return m_Random; }
        }
        public GridTile[,] GridField
        {
            get { return m_GridField; }
        }

        public bool CreativeMode { get; set; }

    }
}
