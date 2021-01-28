using Newtonsoft.Json;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace shooterlandWebBack.Services
{
    public interface IUsersAchievService
    {
        void RefreshUserAchiev(int id, string gameMode);

        UserAchievModel[] UserAchievments(int id);  
    }

    public class UsersAchievService: IUsersAchievService
    {
        private DataContext _context;

        public UsersAchievService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is resposible for the refresh of the achievement of a given IdUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameMode"></param>
        public void RefreshUserAchiev(int id, string gameMode)
        {
            var userStats = _context.StatsSingleplayer.Where(s => s.Id == id && s.GameMode == gameMode ).First();

            var scoreAchievQuery = _context.Achievement.Where(s => s.Type == AchievementType.Score).ToArray();  
            var killsAchievQuery = _context.Achievement.Where(k => k.Type == AchievementType.Kills).ToArray();
            var roundsAchievQuery = _context.Achievement.Where(r => r.Type == AchievementType.Rounds).ToArray();
            
            foreach(Achievement scoreAchiev in scoreAchievQuery)//Highest score in one game
            {
                if (scoreAchiev.Value <= userStats.BestScore)
                {
                    UsersAchievements newUserAchiev = new UsersAchievements
                    {
                        IdUser = userStats.Id,
                        IdAchievement = scoreAchiev.IdAchievement
                    };

                    if (!_context.UsersAchievements.Where(a => a.IdUser == newUserAchiev.IdUser && a.IdAchievement == newUserAchiev.IdAchievement).Any())
                    {
                        _context.UsersAchievements.Add(newUserAchiev);
                        _context.SaveChanges();
                    }
                }
            }

            foreach (Achievement killAchiev in killsAchievQuery)//Highest number of kills in one game
            {
                if (killAchiev.Value <= userStats.HighestKills)
                {
                    UsersAchievements newUserAchiev = new UsersAchievements
                    {
                        IdUser = userStats.Id,
                        IdAchievement = killAchiev.IdAchievement
                    };

                    if (!_context.UsersAchievements.Where(a => a.IdUser == newUserAchiev.IdUser && a.IdAchievement == newUserAchiev.IdAchievement).Any())
                    {
                        _context.UsersAchievements.Add(newUserAchiev);
                        _context.SaveChanges();
                    }
                }
            }
            
            foreach (Achievement roundsAchiev in roundsAchievQuery)//Highest round reached in one game
            {
                if (roundsAchiev.Value <= userStats.HighestRound)
                {
                    UsersAchievements newUserAchiev = new UsersAchievements
                    {
                        IdUser = userStats.Id,
                        IdAchievement = roundsAchiev.IdAchievement
                    };

                    if (!_context.UsersAchievements.Where(a => a.IdUser == newUserAchiev.IdUser && a.IdAchievement == newUserAchiev.IdAchievement).Any())
                    {
                        _context.UsersAchievements.Add(newUserAchiev);
                        _context.SaveChanges();
                    }
                }
            }

        }

        public UserAchievModel[] UserAchievments(int id)
        {
            var userAchievments = _context.UsersAchievements.Where(u => u.IdUser == id);
            var achievments = _context.Achievement;

            var achievList = achievments.Join(
                userAchievments,
                achievments => achievments.IdAchievement,
                userAchievments => userAchievments.IdAchievement,
                (achievments, userAchievments) => new UserAchievModel
                {
                    Description = achievments.Description,
                    Medal = achievments.Medal
                })
                .ToArray();

            return achievList;
        }

    }
}
