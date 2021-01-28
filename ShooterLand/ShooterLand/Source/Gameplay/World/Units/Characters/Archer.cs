using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Units.Characters
{
    class Archer : Character
    {
     
        public Archer(Vector2 _position,int _ownerId) 
            : base("2D\\Characters\\archer", _position, new Vector2(60, 60),_ownerId)
        {
            speed = 3.0f;
            healthMax = 90;
            attackArea = 15.0f;
            damage = 30;
            currentHealth = healthMax;
            range = 350f;
            name = "The Archer";
            description = "Shoots fast arrows that only hit one enemy";
            currentTime = 0;
            cooldown = 0.2f;
            projectileType = "Arrow";

        }
    }
}
