using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.WalkingEnemies
{
    public class GiantSpider : WalkingEnemy
    {

        public GiantSpider(Vector2 _position,int _id, int _attributesMultiplier) : base("2D\\Enemies\\Spider", _position, new Vector2(100, 100), _id)
        {

            speed = 1.0f + 0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 120 + 10 * _attributesMultiplier;
            scoreAwarded = 75;
            damage = 60 + 5 * _attributesMultiplier;
            currentHealth = healthMax;
        }

       


    }
}
