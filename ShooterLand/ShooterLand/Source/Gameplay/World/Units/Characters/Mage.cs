using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Units.Characters
{
    class Mage : Character
    {
        public Mage(Vector2 _position,int _ownerId) : base("2D\\Characters\\mage", _position, new Vector2(32, 32),_ownerId)
        {
            speed = 5.0f;
            healthMax = 130;
            attackArea = 150.0f;
            damage = 50;
            currentHealth = healthMax;
            range = 250f;
            name = "The Fire Mage";
            description = "Shoots slow fireballs that hit every enemy within range";
            currentTime = 0;
            cooldown = 1f;
            projectileType = "Fireball";
        }
    }
}
