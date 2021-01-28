using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Engine.Basic2DObjects;

namespace ShooterLand.Source.Gameplay.Managers
{
    public class TileManager
    {
        private Tile[] arrayTile;
        private int tilesWidht;
        private int tilesHeight;

        public TileManager()
        {
            arrayTile = new Tile[27];
            tilesWidht = 64;
            tilesHeight = 64;

            arrayTile[0] = new Tile("2D\\MapTiles\\DirtTile", true,true,tilesWidht,tilesHeight);
            arrayTile[1] = new Tile("2D\\MapTiles\\GrassTile", true,true, tilesWidht, tilesHeight);
            arrayTile[2] = new Tile("2D\\MapTiles\\WaterTile", false,true, tilesWidht, tilesHeight);
            arrayTile[3] = new Tile("2D\\MapTiles\\WaterDownLeftTile", false,true, tilesWidht, tilesHeight);
            arrayTile[4] = new Tile("2D\\MapTiles\\WaterDownMidTile", false,true, tilesWidht, tilesHeight);
            arrayTile[5] = new Tile("2D\\MapTiles\\WaterDownRightTile", false,true, tilesWidht, tilesHeight);
            arrayTile[6] = new Tile("2D\\MapTiles\\WaterTopLeftTile", false,true, tilesWidht, tilesHeight);
            arrayTile[7] = new Tile("2D\\MapTiles\\WaterTopMidTile", false,true, tilesWidht, tilesHeight);
            arrayTile[8] = new Tile("2D\\MapTiles\\WaterTopRightTile", false,true, tilesWidht, tilesHeight);
            arrayTile[9] = new Tile("2D\\MapTiles\\WaterMidLeftTile", false,true, tilesWidht, tilesHeight);
            arrayTile[10] = new Tile("2D\\MapTiles\\WaterMidRightTile", false,true, tilesWidht, tilesHeight);
            arrayTile[11] = new Tile("2D\\MapTiles\\GrassDown", true, true, tilesWidht, tilesHeight);
            arrayTile[12] = new Tile("2D\\MapTiles\\GrassDownLeft", true, true, tilesWidht, tilesHeight);
            arrayTile[13] = new Tile("2D\\MapTiles\\GrassDownRight", true, true, tilesWidht, tilesHeight);
            arrayTile[14] = new Tile("2D\\MapTiles\\GrassTop", true, true, tilesWidht, tilesHeight);
            arrayTile[15] = new Tile("2D\\MapTiles\\GrassTopLeft", true, true, tilesWidht, tilesHeight);
            arrayTile[16] = new Tile("2D\\MapTiles\\GrassTopRight", true, true, tilesWidht, tilesHeight);
            arrayTile[17] = new Tile("2D\\MapTiles\\GrassLeft", true, true, tilesWidht, tilesHeight);
            arrayTile[18] = new Tile("2D\\MapTiles\\GrassRight", true, true, tilesWidht, tilesHeight);
            arrayTile[19] = new Tile("2D\\MapTiles\\WallDown", false, false, tilesWidht, tilesHeight);
            arrayTile[20] = new Tile("2D\\MapTiles\\WallDownLeft", false, false, tilesWidht, tilesHeight);
            arrayTile[21] = new Tile("2D\\MapTiles\\WallDownRight", false, false, tilesWidht, tilesHeight);
            arrayTile[22] = new Tile("2D\\MapTiles\\WallRight", false, false, tilesWidht, tilesHeight);
            arrayTile[23] = new Tile("2D\\MapTiles\\WallLeft", false, false, tilesWidht, tilesHeight);
            arrayTile[24] = new Tile("2D\\MapTiles\\WallTopLeft", false, false, tilesWidht, tilesHeight);
            arrayTile[25] = new Tile("2D\\MapTiles\\WallTopRight", false, false, tilesWidht, tilesHeight);
            arrayTile[26] = new Tile("2D\\MapTiles\\WallTop", false, false, tilesWidht, tilesHeight);
        }
        public int GetTilesWidht()
        {
            return tilesWidht;
        }

        public int GetTilesHeight()
        {
            return tilesHeight;
        }

        public Tile GetTile(int _position)
        {
            return arrayTile[_position];
        }
    }
}
