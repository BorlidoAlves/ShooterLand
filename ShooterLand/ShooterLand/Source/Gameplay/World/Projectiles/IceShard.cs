using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Projectiles
{
    public class IceShard : Projectile 
    {
        List<Unit> hittedUnits;

        public IceShard(Vector2 _position, Unit _owner, Vector2 _target) : base("2D\\Projectiles\\iceShard", _position, new Vector2(40, 40), _owner, _target)
        {
            speed = 10.00f;
            hittedUnits = new List<Unit>();
        }

        public override void Update(Vector2 _offset, List<Enemy> _enemies,Character _character, MapManager _map)
        {
            position += (direction * speed);

            int mapTileX = (int)position.X / _map.GetTiles().GetTilesWidht();
            int mapTileY = (int)position.Y / _map.GetTiles().GetTilesHeight();

            if (direction.X > 0)
            {
                mapTileX++;
            }

            if (direction.Y > 0)
            {
                mapTileY++;
            }
            int tileType = _map.GetTileType(mapTileY, mapTileX);


            CheckCollision(_enemies,_character);
            //we want an ice shard to affect any enemies that come across his way
            //To implement this behavior, the ice shard should only have the variable done with value true when it went trough all the range
            //of the unit who shot it

            if (OutOfRange() || !_map.GetTiles().GetTile(tileType).ProjectileCross())
            {
                done = true;

            }
        }

        public override bool CheckCollision(List<Enemy> _enemies, Character _character)
        {

            //go trough the list of enemies
            for (int i = 0; i < _enemies.Count; i++)                     
            {

                //if the Unit who shot the projectile and the target are foes 
                //AND if the target is in range of the owner of the projectile attack area
                if (Vector2.Distance(GetPosition(), _enemies[i].GetPosition()) < owner.GetAttackArea() && owner.GetOwnerId() != _enemies[i].GetOwnerId())
                {

                    //if the enemy was already hitted by the projectile it will not suffer the damage again
                    if (!hittedUnits.Contains(_enemies[i]))
                    {
                        _enemies[i].GetHit(owner.GetDamage());
                        hittedUnits.Add(_enemies[i]);
                        return true;
                    }
                }
            }

            if (owner.GetOwnerId() != _character.GetOwnerId() && Vector2.Distance(GetPosition(), _character.GetPosition()) < owner.GetAttackArea())
            {
                if (_character.HasShield())
                {
                    _character.SetShieldStatus(false);
                   
                    return true;
                }
                else
                {
                    _character.GetHit(owner.GetDamage());
                    return true;
                }
            }

            return false;
        }

        
    }
}