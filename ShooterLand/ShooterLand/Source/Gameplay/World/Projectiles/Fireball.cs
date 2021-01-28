using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Projectiles
{
    public class Fireball : Projectile
    {

        public Fireball(Vector2 _position, Unit _owner, Vector2 _target) : base("2D\\Projectiles\\fireball", _position, new Vector2(40, 40), _owner, _target)
        {
            speed = 5.00f;
        }


        public override void Update(Vector2 _offset, List<Enemy> _enemies, Character _character, MapManager _map)
        {
            base.Update(_offset, _enemies, _character, _map);
        }

        //We intend that a fireball has a slightly different behavior than the other projectiles.
        //In other words , we want the fireball to affect all enemies within range
        public override bool CheckCollision(List<Enemy> _enemies, Character _character)
        {
            bool hitted = false;
            for (int i = 0; i < _enemies.Count; i++)
            {

                //if the Unit who shot the projectile and the target are foes and if the target is in range of the owner of the projectile attack area
                if (owner.GetOwnerId() != _enemies[i].GetOwnerId() && Vector2.Distance(GetPosition(), _enemies[i].GetPosition()) < 50)
                {
                    for(int j = 0; j < _enemies.Count; j++)
                    {
                        if(owner.GetOwnerId() != _enemies[j].GetOwnerId()&& Vector2.Distance(GetPosition(), _enemies[j].GetPosition()) < _character.GetAttackArea()){
                            _enemies[j].GetHit(owner.GetDamage());
                        }
                    }
                 

                    hitted = true;

                }
            }

            if (hitted)
            {
                return true;
            }
           

            if (owner.GetOwnerId() != _character.GetOwnerId() && Vector2.Distance(GetPosition(), _character.GetPosition()) < owner.GetAttackArea())
            {
                if (_character.HasShield())
                {
                    _character.SetShieldStatus(false);
                    Debug.WriteLine("Perdeu o escudo");
                    return true;
                }
                else
                {
                    _character.GetHit(owner.GetDamage());
                    return true;
                }

            }

            return false;
        }

        
    }
}