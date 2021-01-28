using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.ShootingEnemies
{
    public class DarkKnight : ShootingEnemy
    {
        

        public DarkKnight( Vector2 _position,int _id ,int _attributesMultiplier) : base("2D\\Enemies\\DarkKnight", _position, new Vector2(100,100),_id)
        {
            speed = 1.0f+0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 300+10 * _attributesMultiplier;
            scoreAwarded = 200;
            damage = 100+5 * _attributesMultiplier;
            currentHealth = healthMax;
            range = 250f;
        }

    }
}

