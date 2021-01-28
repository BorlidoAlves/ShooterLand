using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Boosts;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.SpawnPoints
{
    public class BoostSpawnPoint : SpawnPoint
    {
        private bool spawned;
        
        public BoostSpawnPoint( int _ownerId) : base(_ownerId)
        {
            spawned = false;
        }

        public void SetSpawned(bool _spawned)
        {
            spawned = _spawned;
        }

        public virtual void Update(RoundManager _roundManager,List<Boost> _boosts,Character _character,MapManager _map)
        {
            if (_boosts.Count == 0)
            {
                if (!spawned &&_roundManager.GetRoundKills() == (10 + 5 * (_roundManager.GetCurrentRound())) / 2)
                {
                    SpawnBoost(_boosts,_character,_map);
                    spawned = true;
                }
            }
        }

        private void SpawnBoost(List<Boost> _boosts,Character _character,MapManager _map)
        {
            float number = (float)random.NextDouble();
            Boost tempBoost;
            Vector2 position = _character.GetPosition();        //initial spawn position equals character current position

            int mapTileX = (int)((position.X + 200) / _map.GetTiles().GetTilesWidht()) + 1;
            int mapTileY = (int)(position.Y / _map.GetTiles().GetTilesHeight());
            int  tileType = _map.GetTileType(mapTileY, mapTileX);       

            if (_map.GetTiles().GetTile(tileType).UnitCross())          
            {
                position.X += 200;
            }
            else
            {
                position.X -= 200;
            }

            if (number < 0.1)
            {
                 tempBoost = new Bomb(new Vector2(position.X, _character.GetPosition().Y), ownerId);
                
            }

            else if (number < 0.30)
            {
                tempBoost = new Star(new Vector2(position.X, _character.GetPosition().Y), ownerId);
            }
            else if (number < 0.6)
            {
                tempBoost = new HealthPack(new Vector2(position.X, _character.GetPosition().Y), ownerId);
            }
            else
            {
                 tempBoost = new Shield(new Vector2(position.X, _character.GetPosition().Y), ownerId);
             
            }

            _boosts.Add(tempBoost);
        }
    }
}
