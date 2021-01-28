using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World.Boosts;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.SpawnPoints;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using ShooterLand.Source.Menus;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Text;

namespace ShooterLand.Source.Gameplay.World
{
    public class WorldSinglePlayer : GameWorld
    {
        private List<Enemy> enemies;
        private List<MonsterSpawnPoint> monsterSpawnPoints;
        private BoostSpawnPoint boostSpawnPoint;
        private UI ui;
        private StatsManager statsManager;
        private RoundManager roundManager;
        private EndRoundMenu endRoundMenu;
        private EndRoundManager endRoundManager;
        private List<Boost> boosts;

        public WorldSinglePlayer(Character _character): base(_character)
        {
            character.SetPosition(new Vector2(900, 900));
            statsManager = new StatsManager();
            roundManager = new RoundManager();

            //lists
            enemies = new List<Enemy>();
            monsterSpawnPoints = new List<MonsterSpawnPoint>();
            boosts = new List<Boost>();

            endRoundManager = new EndRoundManager(character);
            ui = new UI();


            endRoundMenu = new EndRoundMenu(NextRoundGame,
                                            endRoundManager.SetPowerUpHealth,
                                            endRoundManager.SetPowerUpDamage,
                                            endRoundManager.SetPowerUpSpeed,
                                            endRoundManager.TakeUpgradeHealth,
                                            endRoundManager.TakeUpgradeDamage,
                                            endRoundManager.TakeUpgradeSpeed,
                                            endRoundManager);

            boostSpawnPoint = new BoostSpawnPoint(1);

            AddSpawnPoints();

        }

        public StatsManager GetStatsManager()
        {
            return statsManager;
        }

        public RoundManager GetRoundManager()
        {
            return roundManager;
        }

        public virtual void Update()
        {
            if (!roundManager.RoundEnded())
            {
                if (!character.IsDead())
                {
                    
                    character.Update(offset, Scroll, map, projectiles,"singleplayer");

                    //update spawn points
                    for (int i = 0; i < monsterSpawnPoints.Count; i++)
                    {
                        monsterSpawnPoints[i].Update(offset, roundManager.GetRoundKills(), roundManager.GetCurrentRound(), projectiles);
                    }

                    boostSpawnPoint.Update(roundManager, boosts, character, map);


                    //update projectiles
                    for (int i = projectiles.Count - 1; i >= 0; i--)
                    {
                        projectiles[i].Update(offset, enemies, character, map);
                        if (projectiles[i].IsDone())
                        {
                            projectiles.RemoveAt(i);
                        }
                    }

                    //update enemies
                    for (int i = enemies.Count - 1; i >= 0; i--)
                    {
                        enemies[i].Update(offset, character, projectiles, map);
                        if (enemies[i].IsDead())
                        {
                            statsManager.SetScore(statsManager.GetScore() + enemies[i].GetScoreAwarded());
                            statsManager.SetTotalKilled(statsManager.GetTotalKilled() + 1);
                            roundManager.SetRoundKills(roundManager.GetRoundKills() + 1);
                            enemies.RemoveAt(i);
                            i--;
                        }
                    }

                    for (int i = boosts.Count - 1; i >= 0; i--)
                    {
                        boosts[i].Update(offset, character,enemies);
                        if (boosts[i].IsDone())
                        {
                            boosts.RemoveAt(i);
                        }
                    }


                    //change round if this one is already finished
                    if (roundManager.ChangeRound(character,ResetScroll))
                    {
                        endRoundManager.GainPowerUp();
                        statsManager.SetScore(statsManager.GetScore() + 500 * (roundManager.GetCurrentRound() - 1));
                        

                    }

                }

                ui.Update(character);
            }
            else
            {
                endRoundMenu.Update();
            }
        }

        public virtual void AddSpawnPoints()
        {
            monsterSpawnPoints.Add(new MonsterSpawnPoint(new Vector2(900, 100), 2, enemies));
            monsterSpawnPoints.Add(new MonsterSpawnPoint(new Vector2(100,900), 2, enemies));
            monsterSpawnPoints[monsterSpawnPoints.Count - 1].GetSpawnTimer().AddToTimer(1000);
            monsterSpawnPoints.Add(new MonsterSpawnPoint(new Vector2(1750, 900), 2, enemies));
            monsterSpawnPoints[monsterSpawnPoints.Count - 1].GetSpawnTimer().AddToTimer(2000);
            monsterSpawnPoints.Add(new MonsterSpawnPoint(new Vector2(900, 1750), 2, enemies));
            monsterSpawnPoints[monsterSpawnPoints.Count - 1].GetSpawnTimer().AddToTimer(3000);


        }

        public void NextRoundGame(object _info)
        {
            projectiles = new List<Projectile>();
            boostSpawnPoint.SetSpawned(false);
            roundManager.NextRound();
            endRoundManager.ResetModCount();
        }

        public void Draw()
        {

            if (!roundManager.RoundEnded())
            {
                map.Draw(offset);


                //update spawn points
                for (int i = 0; i < monsterSpawnPoints.Count; i++)
                {
                    monsterSpawnPoints[i].Draw(offset);
                }

                

                for (int i = 0; i < boosts.Count; i++)
                {
                    boosts[i].Draw(offset);
                }


                character.Draw(offset);

                //draw the projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(offset);
                }

                //draw the units in the field
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(offset);
                }



                ui.DrawSingleplayer(character, statsManager.GetScore(), statsManager.GetTotalKilled(), roundManager.GetCurrentRound());
            }
            else
            {
                endRoundMenu.Draw(character);
            }



        }
    }
}
