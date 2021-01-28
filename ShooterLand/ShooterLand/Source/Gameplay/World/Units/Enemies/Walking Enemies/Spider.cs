using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.WalkingEnemies
{
    public class Spider : WalkingEnemy
    {

        public Spider(Vector2 _position,int _id,int _attributesMultiplier) : base("2D\\Enemies\\Spider", _position, new Vector2(40, 40), _id)
        {
            speed = 4.0f+ 0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 30+10 * _attributesMultiplier;
            currentHealth = healthMax;
            scoreAwarded = 15;
            damage = 10+ 5* _attributesMultiplier;
        }

     
    }
}
