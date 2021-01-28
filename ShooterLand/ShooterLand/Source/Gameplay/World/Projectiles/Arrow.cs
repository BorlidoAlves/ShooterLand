using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Projectiles
{
    public class Arrow : Projectile 
    {

        public Arrow(Vector2 _position, Unit _owner, Vector2 _target) : base("2D\\Projectiles\\arrow", _position, new Vector2(100, 100), _owner, _target)
        {
            speed = 10.00f;
        }

       

    }
}