using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.ShootingEnemies
{
    public class Imp : ShootingEnemy
    {
        

        public Imp( Vector2 _position,int _id ,int _attributesMultiplier) : base("2D\\Enemies\\Imp", _position, new Vector2(40,40),_id)
        {
            speed = 2.0f+0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 60+10 * _attributesMultiplier;
            scoreAwarded = 20;
            damage = 20+5 * _attributesMultiplier;
            currentHealth = healthMax;
            
            range = 250f;
        }
    }
}

