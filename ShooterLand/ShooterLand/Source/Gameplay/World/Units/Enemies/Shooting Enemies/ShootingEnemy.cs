using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.ShootingEnemies
{
    public class ShootingEnemy : Enemy
    {
        private _Timer shootingTimer;
        private bool readyToShoot;
    
        
        
        public ShootingEnemy(string _path, Vector2 _position, Vector2 _dimensions, int _id) : base(_path, _position, _dimensions,_id)
        {
            shootingTimer = new _Timer(1000);
            readyToShoot = true;
        }
       
        public override void Update(Vector2 _offset, Character _character,List<Projectile> _projectiles,MapManager _map)
        {
            ArtificialIntelligence(_character,_projectiles,_map);
            if (!readyToShoot)
            {
                shootingTimer.UpdateTimer();
                if (shootingTimer.Test())
                {
                    readyToShoot = true;
                    shootingTimer.ResetToZero();
                }
            }
            base.Update(_offset, _character, _projectiles,_map); ;
        }


        //the enemy will move to the point where the player currently is
        public override void ArtificialIntelligence(Character _character, List<Projectile> _projectiles,MapManager _map)
        {
            Random random = new Random();

            if (Vector2.Distance(position, _character.GetPosition()) < range)
            {
               // float number = (float)random.NextDouble();
                //if (number > 0.95)
                if(readyToShoot)
                {
                    readyToShoot = false;
                    _projectiles.Add(new Fireball(new Vector2(GetPosition().X, GetPosition().Y), this, new Vector2(_character.GetPosition().X, _character.GetPosition().Y)));
                }
            }
            base.ArtificialIntelligence(_character,_projectiles,_map);
        }

      
        public override void Draw(Vector2 _offset)
        {
            base.Draw(_offset);
        }
    }
}

