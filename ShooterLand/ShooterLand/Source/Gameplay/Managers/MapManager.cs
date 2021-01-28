using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine;

namespace ShooterLand.Source.Gameplay.Managers
{
    public class MapManager
    {
        private TileManager tiles;
        private int[,] map;
        private const int mapWidth = 30;
        private const int mapHeight = 30;


        public MapManager()
        {
            this.map = new int[mapHeight, mapWidth];
            this.tiles = new TileManager();

            FillMap();

        }

        public TileManager GetTiles()
        {
            return tiles;
        }

        public int [,] GetMap()
        {
            return map;
        }

        public int GetTileType(int _x ,int _y)
        {
            return map[_x, _y];
        }
             
        private void FillMap()
        {
            map[0, 0] = 24;
            map[0, 29] = 25;
            map[29, 0] = 20;
            map[29, 29] = 21;

            for (int i = 1; i < mapWidth-1; i++)
            {
                map[0, i] = 26;
                map[mapHeight - 1, i] = 19;
            }

            for (int i = 1; i < mapHeight-1; i++)
            {
                map[i, 0] = 23;
                map[i, mapWidth - 1] = 22;
            }

            

            map[15, 12] = 15;
            map[15, 13] = 14;
            map[15, 14] = 14;
            map[15, 15] = 14;
            map[15, 16] = 16;
            map[16, 16] = 18;
            map[17, 16] = 18;
            map[18, 16] = 18;
            map[19, 16] = 13;
            map[19, 12] = 12;
            map[19, 13] = 11;
            map[19, 14] = 11;
            map[19, 15] = 11;
            map[18, 12] = 17;
            map[17, 12] = 17;
            map[16, 12] = 17;
            map[16, 13] = 6;
            map[16, 14] = 7;
            map[16, 15] = 8;
            map[17, 13] = 9;
            map[17, 14] = 2;
            map[17, 15] = 10;
            map[18, 13] = 3;
            map[18, 14] = 4;
            map[18, 15] = 5;
            
        }
        
       public List<Vector2> GetPathPositions(Vector2 _start,Vector2 _end)
        {
            List<Vector2> positions = new List<Vector2>();
            List<PathUnit> path = GetPath(_start, _end);
            for(int i = 0; i < path.Count; i++)
            {
                int x = path[i].GetTileY();
                int y = path[i].GetTileX();
                int positionX = (tiles.GetTilesWidht() - 1) + tiles.GetTilesWidht() * (x - 1) + tiles.GetTilesWidht() / 2;
                int positionY= (tiles.GetTilesHeight() - 1) + tiles.GetTilesHeight() * (y - 1) + tiles.GetTilesHeight() / 2;
                positions.Add(new Vector2(positionX, positionY));

            }

            return positions;
        }

        public List<PathUnit> GetPath(Vector2 _start, Vector2 _end)           //must convert the positions to the tile they correspond
        {
            List<PathUnit> path = new List<PathUnit>();
            
            PathUnit start = new PathUnit( (int)(_start.Y/GetTiles().GetTilesHeight()), (int)(_start.X / GetTiles().GetTilesWidht()), null,0);

            PathUnit finish = new PathUnit((int)(_end.Y / GetTiles().GetTilesHeight()), (int) (_end.X / GetTiles().GetTilesWidht()) , null, 0);


            start.SetDistance(finish.GetTileX(), finish.GetTileY());

            var activeTiles = new List<PathUnit>();
            activeTiles.Add(start);
            var visitedTiles = new List<PathUnit>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.GetCostDistance()).First();

                if (checkTile.GetTileX() == finish.GetTileX() && checkTile.GetTileY() == finish.GetTileY())     //if it´s the destiny point
                {

                   PathUnit tile = checkTile;
                    while (tile!=null)
                    {
                        path.Add(tile);
                        tile = tile.GetParent();
                    }

                    path.Reverse() ;
                    return path;
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetCrossableTiles( checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.GetTileX() == walkableTile.GetTileX() && x.GetTileY() == walkableTile.GetTileY()))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.GetTileX() == walkableTile.GetTileX() && x.GetTileY() == walkableTile.GetTileY()))
                    {
                        var existingTile = activeTiles.First(x => x.GetTileX() == walkableTile.GetTileX() && x.GetTileY() == walkableTile.GetTileY());
                        if (existingTile.GetCostDistance() > checkTile.GetCostDistance())
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            return path;
        }

        private List<PathUnit> GetCrossableTiles(PathUnit _currentPathUnit, PathUnit _targetPathUnit)
        {
            List<PathUnit> crossableTiles = new List<PathUnit>();

            crossableTiles.Add(new PathUnit(_currentPathUnit.GetTileX(), _currentPathUnit.GetTileY() - 1, _currentPathUnit, _currentPathUnit.GetCost() + 1));
            crossableTiles.Add(new PathUnit(_currentPathUnit.GetTileX(), _currentPathUnit.GetTileY() +1, _currentPathUnit, _currentPathUnit.GetCost() + 1));
            crossableTiles.Add(new PathUnit(_currentPathUnit.GetTileX()-1, _currentPathUnit.GetTileY() , _currentPathUnit, _currentPathUnit.GetCost() + 1));
            crossableTiles.Add(new PathUnit(_currentPathUnit.GetTileX()+1, _currentPathUnit.GetTileY() , _currentPathUnit, _currentPathUnit.GetCost() + 1));

            for(int i = 0; i < crossableTiles.Count; i++)
            {
                crossableTiles[i].SetDistance(_targetPathUnit.GetTileX(), _targetPathUnit.GetTileY());
            }

            for (int i = 0; i < crossableTiles.Count; i++)
            {
               

                

                if (crossableTiles[i].GetTileX() < 0 || crossableTiles[i].GetTileX() > mapHeight - 1
                    || crossableTiles[i].GetTileY() < 0 || crossableTiles[i].GetTileY() > mapWidth - 1
                    ||!tiles.GetTile(map[crossableTiles[i].GetTileX(), crossableTiles[i].GetTileY()]).UnitCross() )
                {
                    crossableTiles.RemoveAt(i);
                }
            }

            return crossableTiles;
        }
        public void Draw(Vector2 _offset)
        {
            Rectangle position = new Rectangle(0, 0, this.tiles.GetTilesWidht(), this.tiles.GetTilesHeight());

            for (int row = 0; row < mapHeight; row++)
            {

                for (int col = 0; col < mapWidth; col++)
                {


                    tiles.GetTile(map[row, col]).Draw(_offset, position);
                    position.X += tiles.GetTilesWidht();
                }
                position.X = 0;
                position.Y += tiles.GetTilesHeight();

            }
        }
    }
}