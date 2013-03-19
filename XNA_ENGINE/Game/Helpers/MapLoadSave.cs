﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using XNA_ENGINE.Engine.Scenegraph;
using XNA_ENGINE.Game.Objects;
using XNA_ENGINE.Game.Scenes;

namespace XNA_ENGINE.Game.Helpers
{
    class MapLoadSave
    {
        //Singleton implementation
        private static MapLoadSave m_MapLoadSave;

        // Generate a new Map
        private const int MAX_HEIGHT = 30;
        private const int MAX_WIDTH = 30;

        private GridTile[,] m_GridField;

        private MapLoadSave()
        {
            m_GridField = new GridTile[MAX_WIDTH, MAX_HEIGHT];
        }

        static public MapLoadSave GetInstance()
        {
            if (m_MapLoadSave == null)
                m_MapLoadSave = new MapLoadSave();

            return m_MapLoadSave;
        }

        public List<List<GridTile>> LoadMap(string XMLFile, GameScene pGameScene)
        {
            List<List<GridTile>> gridField = new List<List<GridTile>>();
            List<GridTile> addTest = new List<GridTile>();
            addTest.Add(new GridTile(pGameScene,0,0));
            addTest.Add(new GridTile(pGameScene, 1, 0));
            addTest.Add(new GridTile(pGameScene, 2, 0));
            gridField.Add(addTest);
            addTest.Clear();
            addTest.Add(new GridTile(pGameScene, 0, 1));
            addTest.Add(new GridTile(pGameScene, 1, 1));
            gridField.Add(addTest);

            System.IO.Stream stream = TitleContainer.OpenStream(XMLFile);
            XDocument doc = XDocument.Load(stream);
            /*
            gridField = (from tile in doc.Descendants("tile")
                       select new GridTile(pGameScene,Convert.ToInt32(tile.Element("positionX").Value),Convert.ToInt32(tile.Element("positionY").Value))
                       {
                         /*  position = new Vector2(Convert.ToInt32(tile.Element("positionX").Value), Convert.ToInt32(tile.Element("positionY").Value)),
                           type = Convert.ToString(tile.Element("type").Value),
                           settlement = Convert.ToString(tile.Element("settlement").Value)*/
                //       }).ToList();*/

            return gridField;
        }

        public void GenerateMap(GameScene pGameScene)
        {
            // Map:
            // Width = 30
            // Height = 30
            List<GridTile> addTest = new List<GridTile>();

            for (int height = 0; height < MAX_HEIGHT; ++height)
            {
                for (int width = 0; width < MAX_WIDTH; ++width)
                {
                    addTest.Add(new GridTile(pGameScene, width, height));
                }
            }

            // Add Tiles to the right grid and give them the right attributes.
            int teller = 0;

            for (int height = 0; height < MAX_HEIGHT; ++height)
            {
                for (int width = 0; width < MAX_WIDTH; ++width)
                {
                    m_GridField[width,height] = addTest.ElementAt(teller);

                    // Set the tile type (normal, tribe,...)
                    m_GridField[width,height].SetTileType("normal");

                    // Use a color to create a new Tribe (none, green, red, blue,...)
                    m_GridField[width,height].SetTileSettlement("none");

                    teller++;
                }
            }

            // Save to XML file
            SaveMap();
        }

        public void SaveMap()
        {
            var xmlFile = new FileStream("GeneratedTileMap.xml", FileMode.OpenOrCreate, FileAccess.Write);
            var writer = new StreamWriter(xmlFile);

            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            writer.WriteLine("<tilemap>");

            for (int height = 0; height < MAX_HEIGHT; ++height)
            {
                for (int width = 0; width < MAX_WIDTH; ++width)
                {
                    writer.WriteLine("<tile>");

                    writer.WriteLine("\t<positionX>" + width + "</positionX>");
                    writer.WriteLine("\t<positionY>" + height + "</positionY>");
                    writer.WriteLine("\t<type>" + m_GridField[height, width].GetTileType() + "</type>");
                    writer.WriteLine("\t<settlement>" + m_GridField[height, width].GetTileSettlement() + "</settlement>");

                    writer.WriteLine("</tile>");
                }
            }

            writer.WriteLine("</tilemap>");

            writer.Close();
            xmlFile.Close();
        }
    }
}

/*
            // ------------------------------------------
            // OPEN AND READ XML FILE
            // ------------------------------------------
            // the file must be available in the Debug (or release) folder
            System.IO.Stream stream = TitleContainer.OpenStream("tilemap.xml");

            XDocument doc = XDocument.Load(stream);

            m_Tiles = new List<Tile>();

            m_Tiles = (from tile in doc.Descendants("tile")
                       select new Tile()
                       {
                           position = new Vector2(Convert.ToInt32(tile.Element("positionX").Value), Convert.ToInt32(tile.Element("positionY").Value)),
                           type = Convert.ToString(tile.Element("type").Value),
                           settlement = Convert.ToString(tile.Element("settlement").Value)
                       }).ToList();

            // Test if the xml reader worked (and it does)
            System.Diagnostics.Debug.WriteLine("Count: " + m_Tiles.ElementAt(0).type);
            // ------------------------------------------
            // END READING XML FILE
            // -----------------------------------------*/