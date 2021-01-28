using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using ShooterLand.Source.Gameplay.World.Units.Enemies.ShootingEnemies;
using ShooterLand.Source.Gameplay.World.Units.Enemies.WalkingEnemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShooterLand.Source.Gameplay.World.SpawnPoints
{
    public class MonsterSpawnPoint : SpawnPoint
    {
        private List<Enemy> units;
        private Vector2 position;
        private Basic2D cave;

        public MonsterSpawnPoint(Vector2 _position, int _ownerId, List<Enemy> _enemies) : base( _ownerId)
        {
            units = _enemies;
            position = _position;
            cave= new Basic2D("2D\\cave",_position, new Vector2(100, 100));
        }

        public virtual void Update(Vector2 _offset, int _roundKills, int _currentRound, List<Projectile> _projectiles)
        {
            int roundKills = _roundKills;
            spawnTimer.UpdateTimer();

            if (roundSpawns < 10 + 5 * _currentRound)
            {
                if (spawnTimer.Test())
                {
                    if (roundSpawns < 10 + 5 * _currentRound - 1)
                    {
                        SpawnEnemy(_currentRound, _projectiles);
                        roundSpawns++;
                        spawnTimer.ResetToZero();
                    }
                    else if (roundKills == 10 + 5 * _currentRound - 1)
                    {
                         SpawnBoss(_currentRound);
                        roundSpawns++;
                        spawnTimer.ResetToZero();
                    }

                }
            }

        }

        public virtual void SpawnEnemy(int _currentRound, List<Projectile> _projectiles)
        {

            float number = (float)random.NextDouble();
            Enemy tempEnemy;

            if (number < 0.5)
            {
                tempEnemy = new Spider(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }
            else if (number < 0.8)
            {
                tempEnemy = new Imp(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }
            else
            {
                tempEnemy = new GiantSpider(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }


            units.Add(tempEnemy);

        }

        public virtual void SpawnBoss(int _currentRound)
        {
            Enemy tempEnemy;
            Debug.WriteLine("BOSS SPAWN RONDA: " + _currentRound);
            if (_currentRound % 3 == 0)
            {
                tempEnemy = new UndeadLord(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }
            else if (_currentRound == 1 || (_currentRound - 1) % 3 == 0)
            {
                tempEnemy = new ArachVille(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }
            else
            {
                tempEnemy = new DarkKnight(new Vector2(position.X, position.Y), ownerId, _currentRound - 1);
            }

            units.Add(tempEnemy);

        }

        public virtual void Draw(Vector2 _offset)
        {
            cave.Draw(_offset);
        }
            

    }
}
