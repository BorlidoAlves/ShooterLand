using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies.WalkingEnemies
{
    public class ArachVille : WalkingEnemy
    {

        public ArachVille(Vector2 _position,int _id, int _attributesMultiplier) : base("2D\\Enemies\\ArachVille", _position, new Vector2(150, 150), _id)
        {
            speed = 4.0f + 0.1f * _attributesMultiplier;
            attackArea = 30.0f;
            healthMax = 250 + 10 * _attributesMultiplier;
            currentHealth = healthMax;
            scoreAwarded = 200;
            damage = 80 + 5 * _attributesMultiplier;
        }
    }
}
