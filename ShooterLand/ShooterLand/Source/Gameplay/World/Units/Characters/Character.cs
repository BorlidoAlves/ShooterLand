using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Engine.Server;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Units.Characters
{
    public class Character : Unit 
    {
        protected _Keyboard keyboard;
        protected string name;
        protected string description;
        protected bool shield;
        protected double currentTime;
        protected float cooldown;
        protected bool readyToShoot;
        protected string projectileType;
        protected bool invincible;
        protected _Timer invicibleTimer;


        public Character(string _path, Vector2 _position, Vector2 _dimensions, int _ownerId) : base(_path, _position, _dimensions, _ownerId)
        {
            keyboard = new _Keyboard();
            shield = false;
            readyToShoot = true;
            invincible = false;
            invicibleTimer = new _Timer(10000);

        }

        public Character()
        {

        }

        public Character(Character other)
        {
            keyboard = other.keyboard;
            name = other.name;
            description = other.description;
            shield = other.shield;
            currentTime = other.currentTime;
            cooldown = other.cooldown;
            readyToShoot = other.readyToShoot;
            isDead = other.isDead;
            speed = other.speed;
            attackArea = other.attackArea;
            damage = other.damage;
            ownerId = other.ownerId;
            range = other.range;
            rotation = other.rotation;
            position = other.position;
            dimensions = other.dimensions;
            model = other.model;
            mouseControl = other.mouseControl;
            healthMax = other.healthMax;
            currentHealth = healthMax;
            projectileType = other.projectileType;
            invincible = other.invincible;
            invicibleTimer = other.invicibleTimer;


    }

        public string GetName()
        {
            return name;
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetShieldStatus(bool _status)
        {
            shield = _status;
        }
        public bool HasShield()
        {
            return shield;
        }

        public bool IsInvicible()
        {
            return invincible;
        }
        public void SetInvincible(bool _inv)
        {
            invincible = _inv;
        }

        public virtual  void Update(Vector2 _offset,PassObject _scroll,MapManager map, List<Projectile> _projectiles,string _gameMode)
        {
            // base.Update(_offset, _scroll, map, _projectiles, _gameMode);
            if (invincible)
            {
                invicibleTimer.UpdateTimer();
                if (invicibleTimer.Test())
                {
                    invincible = false;
                    invicibleTimer.ResetToZero();
                }
            }
            if (currentTime + cooldown < Globals.gameTime.TotalGameTime.TotalSeconds)
            {
                readyToShoot = true;
            }

            Move(_scroll,map,_gameMode);
            Shoot(_projectiles,_offset,_gameMode);
        }

       
        private void Move(PassObject _scroll,MapManager map, string _gameMode)
        {
            bool checkScroll = false;
            int mapTileX, mapTileY, tileType;

            keyboard.Update();
            if (keyboard.GetPress("A"))
            {
                mapTileX = (int)((position.X - speed) / map.GetTiles().GetTilesWidht());
                mapTileY = (int)(position.Y / map.GetTiles().GetTilesHeight());
                
                tileType = map.GetTileType(mapTileY, mapTileX);

                if (map.GetTiles().GetTile(tileType).UnitCross())
                {
                    SetPosition(new Vector2(position.X - speed, position.Y));
                    checkScroll = true;
                }

            }
            if (keyboard.GetPress("D"))
            {
                mapTileX = (int)((position.X + speed) / map.GetTiles().GetTilesWidht()) + 1;
                mapTileY = (int)(position.Y / map.GetTiles().GetTilesHeight());
               
                tileType = map.GetTileType(mapTileY, mapTileX);

                if (map.GetTiles().GetTile(tileType).UnitCross())
                {
                    SetPosition(new Vector2(position.X + speed, position.Y));
                    checkScroll = true;
                }

            }
            if (keyboard.GetPress("W"))
            {
                mapTileX = (int)position.X / map.GetTiles().GetTilesWidht();
                mapTileY = (int)(position.Y - speed) / map.GetTiles().GetTilesHeight();
                
                tileType = map.GetTileType(mapTileY, mapTileX);

                if (map.GetTiles().GetTile(tileType).UnitCross())
                {
                    SetPosition(new Vector2(GetPosition().X, GetPosition().Y - speed));  //Y is inverted, this means that 0 is on top .So if the player wants to go up Y has do decrease 
                    checkScroll = true;
                }

            }

            if (keyboard.GetPress("S"))
            {
                mapTileX = (int)position.X / map.GetTiles().GetTilesWidht();
                mapTileY = (int)(position.Y + speed) / map.GetTiles().GetTilesHeight() + 1;
                
                
                tileType = map.GetTileType(mapTileY, mapTileX);

                if (map.GetTiles().GetTile(tileType).UnitCross())
                {
                    SetPosition(new Vector2(GetPosition().X, GetPosition().Y + speed));   //Again, Y is inverted
                    checkScroll = true;
                }

            }

            if (checkScroll)
            {
                _scroll(GetPosition());
            }

            if (_gameMode == "multiplayer")
            {
                ClientSend.PlayerMovement(GetPosition(), GetRotation());
            }

            keyboard.UpdateOld();
        }

        private void Shoot(List<Projectile> _projectiles,Vector2 _offset,string _gameMode)
        {
            mouseControl.Update();
            if (mouseControl.LeftClick())

                if (readyToShoot)
                {
                   
                    Projectile tempProjectile = null;
                    string type = "";

                    if (projectileType.Equals("Arrow"))
                    {
                        tempProjectile = new Arrow(new Vector2(GetPosition().X, GetPosition().Y), this, new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y) - _offset);
                        type = "arrow";
                    }
                    else if (projectileType.Equals("IceShard"))
                    {
                        tempProjectile = new IceShard(new Vector2(GetPosition().X, GetPosition().Y), this, new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y) - _offset);
                        type = "ice";
                    }
                    else if (projectileType.Equals("Fireball"))
                    {
                        tempProjectile = new Fireball(new Vector2(GetPosition().X, GetPosition().Y), this, new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y) - _offset);
                        type = "fire";
                    }

                    if (_gameMode == "multiplayer")
                    {
                      ClientSend.PlayerShoot(position, new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y) - _offset,type);
                    }

                    _projectiles.Add(tempProjectile);
                    currentTime = Globals.gameTime.TotalGameTime.TotalSeconds;
                    readyToShoot = false;
                }

            SetRotation(RotateTowards(GetPosition(), new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y) - _offset));
            mouseControl.UpdateOld();
        }

        public override void GetHit(int _damageTaken)
        {
            if (!invincible)
            {
                
                //invicibleTimer.ResetToZero();

                if (_damageTaken >= currentHealth)
                {
                    currentHealth = 0;
                    isDead = true;
                }
                else
                {
                    currentHealth -= _damageTaken;
                }
            }
           
        }

        public override void Draw(Vector2 _offset)
        {
            base.Draw(_offset);
        }

    }
}
   
