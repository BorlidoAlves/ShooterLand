using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.Boosts
{
    public class Bomb :Boost
    {
        public Bomb(Vector2 _position, int _id) : base("2D\\Boosts\\bomb", _position, new Vector2(50, 50), _id)
        {

        }

        public override void Update(Vector2 _offset, Character _character,List<Enemy> _enemies)
        {
            if (VerifyCatch(_character))
            {
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].SetLifeStatus(true);
                }
                done = true;
            }
            

            base.Update(_offset, _character,_enemies);
        }
    }
}
