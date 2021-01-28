using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Projectiles
{
    public class Projectile : Basic2D
    {
        protected bool done;            //the projectile travelled all the character range or hitted an enemy
        protected float speed;          //speed in which the projectile will travel 
        protected Vector2 direction;
        protected Unit owner;           //Unit that shot the projectile
        protected Vector2 originalPosition;  //position where the projectile was shot


        public Projectile(string _path, Vector2 _position, Vector2 _dimensions, Unit _owner, Vector2 _target) : base(_path, _position, _dimensions)
        {
            done = false;
            owner = _owner;
            direction = _target - owner.GetPosition();
            originalPosition = _position;
            direction.Normalize(); //makes lenght 1
            SetRotation(RotateTowards(GetPosition(), new Vector2(_target.X, _target.Y)));
        }

        

        public bool IsDone()
        {
            return done;
        }
        public virtual void Update(Vector2 _offset, List<Enemy> _enemies, Character _character,MapManager _map)
        {
            
            position += (direction * speed);
            int  mapTileX = (int)position.X / _map.GetTiles().GetTilesWidht();
            int mapTileY = (int)position.Y  / _map.GetTiles().GetTilesHeight();

            if (direction.X > 0)
            {
                mapTileX++;
            }

            if (direction.Y > 0)
            {
                mapTileY++;
            }
            int tileType = _map.GetTileType(mapTileY, mapTileX);

            if (CheckCollision(_enemies, _character) || OutOfRange() || !_map.GetTiles().GetTile(tileType).ProjectileCross())
            {
                done = true;

            }
        }

        public virtual void UpdateMultiplayer(Vector2 _offset,Character _myCharacter,Character _rivalCharacter,MapManager _map)
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

            if (CheckCollisionMultiPlayer(_myCharacter, _rivalCharacter) || OutOfRange() || !_map.GetTiles().GetTile(tileType).UnitCross())
            {
                done = true;

            }
        }

        public virtual bool CheckCollisionMultiPlayer(Character _myCharacter,Character _rivalCharacter)
        {
            if (owner.GetOwnerId() != _myCharacter.GetOwnerId() && Vector2.Distance(GetPosition(), _myCharacter.GetPosition()) < owner.GetAttackArea())
            {
                //if an enemy is hit there was a collision .This implies that the variable done will be set as true
                //The fact that the return is inside the if condition implies that this projectile will only affect one enemy
                _myCharacter.GetHit(owner.GetDamage());
                return true;
            }

            if (owner.GetOwnerId() != _rivalCharacter.GetOwnerId() && Vector2.Distance(GetPosition(), _rivalCharacter.GetPosition()) < owner.GetAttackArea())
            {
                
                _rivalCharacter.GetHit(owner.GetDamage());
                return true;
            }

            return false;
        }


        public virtual bool CheckCollision(List<Enemy> _enemies, Character _character)
        {
            for (int i = 0; i < _enemies.Count; i++)                     //will go trough the list of enemies
            {
                //if the Unit who shot the projectile and the target are foes 
                //AND if the target is in range of the owner of the projectile attack area
                if (owner.GetOwnerId() != _enemies[i].GetOwnerId() && Vector2.Distance(GetPosition(), _enemies[i].GetPosition()) < owner.GetAttackArea())
                {

                    //if an enemy is hit there was a collision .This implies that the variable done will be set as true
                    //The fact that the return is inside the if condition implies that this projectile will only affect one enemy
                    _enemies[i].GetHit(owner.GetDamage());
                    return true;
                }
            }

            if (owner.GetOwnerId() != _character.GetOwnerId() && Vector2.Distance(GetPosition(), _character.GetPosition()) < owner.GetAttackArea())
            {
                if (_character.HasShield())
                {
                    _character.SetShieldStatus(false);
                    Debug.WriteLine("Perdeu o escudo");
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

       

        public virtual bool OutOfRange()
        {
            if (Vector2.Distance(GetPosition(), originalPosition) > owner.GetRange())
            {
                return true;
            }

            return false;

        }

      
    }
}