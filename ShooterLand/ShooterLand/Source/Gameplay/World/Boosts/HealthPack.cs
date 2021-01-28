using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.Boosts
{
    public class HealthPack : Boost
    {

        public HealthPack(Vector2 _position, int _id) : base("2D\\Boosts\\HealthPack", _position, new Vector2(50, 50), _id)
        {

        }
        public override void Update(Vector2 _offset, Character _character,List<Enemy>_enemies)
        {
            if (VerifyCatch(_character))
            {
                _character.Revive();
                done = true;
            }

            base.Update(_offset, _character,_enemies);
        }

       
    }


}
