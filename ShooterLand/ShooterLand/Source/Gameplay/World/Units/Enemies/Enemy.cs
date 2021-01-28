using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShooterLand.Source.Gameplay.World.Units.Enemies
{
    public class Enemy : Unit
    {
        protected int scoreAwarded;
        protected Vector2 moveTo;
        protected List<Vector2> path;
        protected _Timer timer;
        protected bool pathing;
        
        public Enemy(string _path, Vector2 _position, Vector2 _dimensions, int _id) : base(_path, _position, _dimensions,_id)
        {
            moveTo = new Vector2(position.X,position.Y);
            path = new List<Vector2>();
            timer = new _Timer(3000);
            pathing = false;
        }

        public int GetScoreAwarded()
        {
            return scoreAwarded;
        }
       
        public virtual void Update(Vector2 _offset, Character _character, List<Projectile> _projectiles,MapManager _map)
        {
            ArtificialIntelligence(_character, _projectiles,_map);
            
        }

        public void FindPath(MapManager _map,Vector2 _start,Vector2 _end,Character _character){
            path.Clear();
            path = _map.GetPathPositions(_start, _end);
            List<Vector2> tempPath = new List<Vector2>();
            tempPath.Add(position);
            for (int i = 1; i < path.Count - 1; i++)
            {
                tempPath.Add(path[i]);
            }
            tempPath.Add(_character.GetPosition());
            //path.RemoveAt(0);
            //path.Insert(0, position);
            // path.RemoveAt(path.Count - 1);
            // path.Add(_character.GetPosition());

            path = tempPath;

        }

        public void MoveUnit()
        {
            
            if(position.X != moveTo.X || position.Y != moveTo.Y)
            {
                rotation = RotateTowards(position, moveTo);
                position += RadialMovement(moveTo, position, speed);
            }
            else if (path.Count > 0)
            {
                moveTo = path[0];
                path.RemoveAt(0);
                position += RadialMovement(moveTo, position, speed);
            }

            
        }


       
        public virtual void ArtificialIntelligence(Character _character, List<Projectile> _projectiles,MapManager _map)
        {
            // SetPosition(GetPosition() + RadialMovement(_character.GetPosition(), this.GetPosition(), this.speed));
            // SetRotation(RotateTowards(this.GetPosition(), _character.GetPosition()));
            timer.UpdateTimer();
            if (path == null || (path.Count == 0 && position.X == moveTo.X && position.Y == moveTo.Y) || timer.Test())
            {
                if (!pathing)
                {
                    Task findPathTask = new Task(() =>
                      {
                          pathing = true;
                          FindPath(_map, position, _character.GetPosition(),_character);
                          moveTo = path[0];
                          path.RemoveAt(0);
                          timer.ResetToZero();
                          pathing = false;
                      });
                    findPathTask.Start();

                }



            }

            else
            {
                MoveUnit();

                if (Vector2.Distance(this.GetPosition(), _character.GetPosition()) < this.GetAttackArea())
                {
                    if (_character.HasShield())
                    {
                        _character.SetShieldStatus(false);
                        Debug.WriteLine("Perdeu o escudo");
                        isDead = true;
                    }
                    else
                    {
                        _character.GetHit(GetDamage());
                        isDead = true;      //replace by character resistant to damage  for x seconds
                    }

                }
            }

        }

        public Vector2 RadialMovement(Vector2 focus, Vector2 position, float speed)       //focus is the destination point
        {
            float distance = Vector2.Distance(position, focus);

            if (distance <= speed)
            {
                return focus - position;
            }

            else
            {
                return (focus - position) * speed / distance;
            }

        }
        public override void Draw(Vector2 _offset)
        {
            base.Draw(_offset);
        }
    }
}

