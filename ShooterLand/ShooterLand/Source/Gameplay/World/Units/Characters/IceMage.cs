using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Units.Characters
{
    class IceMage : Character
    {
        public IceMage(Vector2 _position,int _ownerId) : base("2D\\Characters\\IceMage", _position, new Vector2(60, 60),_ownerId)
        {
            speed = 4.0f;
            healthMax = 100;
            attackArea = 35.0f;
            damage = 20;
            currentHealth = healthMax;
            range = 350f;
            name = "The Ice Mage";
            description = "Shoots medium speed ice shards that hit every enemy that crosses their way";
            currentTime = 0;
            cooldown = 0.5f;
            projectileType = "IceShard";

        }

    }
}
