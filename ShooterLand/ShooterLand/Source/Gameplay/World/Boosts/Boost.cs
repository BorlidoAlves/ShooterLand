using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System.Collections.Generic;

namespace ShooterLand.Source.Gameplay.World.Boosts
{
        public class Boost : Basic2D
        {
            protected int ownerId;
            protected bool done;

            public Boost(string _path, Vector2 _position, Vector2 _dimensions, int _id) : base(_path, _position, _dimensions)
            {
                ownerId = _id;
                done = false;
            }

            public int GetOwnerId()
            {
                return ownerId;
            }

            public bool IsDone()
            {
                return done;
            }

            public virtual void Update(Vector2 _offset, Character _character,List<Enemy> _enemies)
            {

                
            }


            public bool VerifyCatch(Character _character)
            {
                if (ownerId == _character.GetOwnerId() && Vector2.Distance(GetPosition(), _character.GetPosition()) < 40)
                {


                    return true;
                }

                return false;
            }

            
        }
    }


