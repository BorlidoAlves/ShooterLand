using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.Units
{
    
    public class Unit : Basic2D
    {
        protected bool isDead;
        protected float speed;
        protected float attackArea;           
        protected int damage;
        protected int currentHealth, healthMax;
        protected int ownerId;
        protected float range;
        private bool hitted;
        protected _Timer hittedTimer;


        public Unit(string _path, Vector2 _position, Vector2 _dimensions,int _ownerId) : base(_path, _position, _dimensions)
        {
            isDead = false;
            ownerId = _ownerId;
            hitted = false;
            hittedTimer = new _Timer(2000);

        }

        public Unit()
        {

        }

        public int GetDamage()
        {
            return damage;
        }

        public float GetAttackArea()
        {
            return attackArea;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void SetLifeStatus(bool _status)
        {
            isDead = _status;
        }
        public int GetHealth()
        {
            return currentHealth;
        }
        public void Revive()
        {
            currentHealth = healthMax;
        }
        public int GetHealthMax()
        {
            return healthMax;
        }

        public float GetSpeed()
        {
            return speed;
        }
       
        public float GetRange()
        {
            return range;
        }
      
        public int GetOwnerId()
        {
            return ownerId;
        }

        public void SetOwnerId(int _ownerId)
        {
            ownerId = _ownerId;   
        }

        public void SetDamage(int _damage)
        {
            this.damage = _damage;
        }
        public void SetMaxHealth(int _maxHealth)
        {
            this.healthMax = _maxHealth;
        }
        public void SetHealth(int _health)
        {
            this.currentHealth = _health;
        }
        public void SetSpeed(float _speed)
        {
            this.speed = _speed;
        }
        
        
        public virtual void GetHit(int _damageTaken)
        {
            hitted = true;
            hittedTimer.ResetToZero();


            if (_damageTaken >= currentHealth)
            {
                currentHealth = 0;
                isDead = true;
            }
            else
            {
                currentHealth -= _damageTaken;
            }
        }

       /* public virtual void Update(Vector2 _offset, PassObject _scroll, MapManager map, List<Projectile> _projectiles,string _gameMode)
        {
            if (hitted)
            {
                hittedTimer.UpdateTimer();
                if (hittedTimer.Test())
                {
                    hitted = false;
                    hittedTimer.ResetToZero();
                }
            }
        }*/

       /* public override void Draw(Vector2 _offset)
        {
            base.Draw(_offset);
            if (hitted)
            {
                Globals.hitEffect.Parameters["SINLOC"].SetValue(((float)Math.Sin((float)hittedTimer.GetTimer() / (float)hittedTimer.GetMSec()
                    + (float)Math.PI / 2) * ((float)Math.PI * 3)));
                Globals.hitEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                Globals.hitEffect.CurrentTechnique.Passes[0].Apply();

            }
        }
       */

    }
}
