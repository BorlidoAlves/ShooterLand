using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.ShootingEnemies
{
    public class UndeadLord : ShootingEnemy
    {
        

        public UndeadLord( Vector2 _position,int _id ,int _attributesMultiplier) : base("2D\\Enemies\\UndeadLord", _position, new Vector2(100,100),_id)
        {
            speed = 2.0f+0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 275+10 * _attributesMultiplier;
            scoreAwarded = 200;
            damage = 90+5 * _attributesMultiplier;
            currentHealth = healthMax;
            range = 225f;
        }

    }
}

