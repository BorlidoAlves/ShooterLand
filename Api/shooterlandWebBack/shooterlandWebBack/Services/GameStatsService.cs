using Microsoft.Data.SqlClient;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Models;
using shooterlandWebBack.Models.Leaderboards;
using shooterlandWebBack.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Services
{
    public interface IGameStatsService
    {
        void UpdateStats(StatUpdateModel stats, int id);
        void UpdateStatsMultiplayer(StatUpdateMultiplayerModel stats, int id);
        LeaderboardScoreModel[] LeaderboardScoreSinglePlayer();
        LeaderboardRoundsModel[] LeaderboardRoundsSinglePlayer();
        LeaderboardKillsModel[] LeaderboardKillsSinglePlayer();
        LeaderboardMultiplayerModel[] LeaderboardMultiplayer();
        UserStatsModel UserGameStats(int id); 
    }
    public class GameStatsService : IGameStatsService
    {
        private DataContext _context;

        public GameStatsService(DataContext context)
        {
            _context = context;
        }

        public LeaderboardMultiplayerModel[] LeaderboardMultiplayer()
        {
            var stats = _context.StatsMultiplayer.Where(s => s.GameMode == "MultiPlayer");
            var users = _context.Users.Where(s => s.Type == "User");

            var leaderboardMulti = users.Join(
                stats,
                user => user.Id,
                stat => stat.Id,
                (user, stat) => new LeaderboardMultiplayerModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Wins = stat.Wins,
                    Defeats = stat.Defeats,
                    WinRate = stat.WinRate
                })
                .OrderByDescending(s => s.Wins)
                .ToArray();

            return leaderboardMulti;
        }

        public LeaderboardKillsModel[] LeaderboardKillsSinglePlayer()
        {
            var stats = _context.StatsSingleplayer.Where(s => s.GameMode=="SinglePlayer");
            var users = _context.Users.Where(s => s.Type == "User");

            var leaderboardKills = users.Join(
                stats,
                user => user.Id,
                stat => stat.Id,
                (user, stat) => new LeaderboardKillsModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Kills = stat.HighestKills
                })
                .OrderByDescending(s => s.Kills)
                .ToArray();

            return leaderboardKills;
        }

        public LeaderboardRoundsModel[] LeaderboardRoundsSinglePlayer()
        {
            var stats = _context.StatsSingleplayer.Where(s => s.GameMode == "SinglePlayer");
            var users = _context.Users.Where(s => s.Type == "User");

            var leaderboardRounds = users.Join(
                stats,
                user => user.Id,
                stat => stat.Id,
                (user, stat) => new LeaderboardRoundsModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Rounds = stat.HighestRound
                })
                .OrderByDescending(s => s.Rounds)
                .ToArray();

            return leaderboardRounds;
        }

        public LeaderboardScoreModel[] LeaderboardScoreSinglePlayer()
        {
            var stats = _context.StatsSingleplayer.Where(s => s.GameMode == "SinglePlayer");
            var users = _context.Users.Where(s => s.Type == "User");

            var leaderboardScore = users.Join(
                stats,
                user => user.Id,
                stat => stat.Id,
                (user, stat) => new LeaderboardScoreModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Score = stat.BestScore
                })
                .OrderByDescending(s => s.Score)
                .ToArray();

            return leaderboardScore;
        }

        public void UpdateStats(StatUpdateModel stats, int id)
        {
            if (stats.GameMode != "SinglePlayer")
            {
                throw new AppException("The Game Mode was to be 'SinglePlayer'");
            }

            var statsId = _context.StatsSingleplayer.Where(s => s.GameMode == stats.GameMode && s.Id == id).First();
            var originStats = _context.StatsSingleplayer.Find(statsId.IdStat);

            if (stats.MonstersKilled < 0)
            {
                throw new AppException("The number of monsters kills cant negative");
            }

            if (originStats == null)
            {
                throw new AppException("Stat does not exist");
            }
            else
            {
            
                originStats.MonstersKilled += stats.MonstersKilled;           
                originStats.GamesPlayed++;

                if (originStats.BestScore < stats.Score)
                {
                    originStats.BestScore = stats.Score;    
                }
                
                if (originStats.HighestRound < stats.Round)
                {
                    originStats.HighestRound = stats.Round;
                }
                
                if (originStats.HighestKills < stats.MonstersKilled)  
                {
                    originStats.HighestKills = stats.MonstersKilled;    
                }

                _context.StatsSingleplayer.Update(originStats);    
                _context.SaveChanges();
                
            }
            
        }

        public void UpdateStatsMultiplayer(StatUpdateMultiplayerModel stats, int id)
        {
            if (stats.GameMode != "MultiPlayer")
            {
                throw new AppException("The Game Mode was to be 'MultiPlayer'");
            }

            var statsId = _context.StatsMultiplayer.Where(s => s.GameMode == stats.GameMode && s.Id == id).First();
            var originStats = _context.StatsMultiplayer.Find(statsId.IdStatMulti);

            if (stats.Result != "Win" && stats.Result != "Defeat")
            {
                throw new AppException("The result was to be 'Win' or 'Defeat'");
            }

            if (originStats == null)
            {
                throw new AppException("Stat does not exist");
            }
            else
            {
                originStats.GamesPlayed++;

                if(stats.Result == "Win")
                {
                    originStats.Wins++;
                }
                else
                {
                    originStats.Defeats++;
                }

                originStats.WinRate = (int)((float)originStats.Wins / (float)originStats.GamesPlayed * 100); 

                _context.StatsMultiplayer.Update(originStats);
                _context.SaveChanges();

            }

        }

        public UserStatsModel UserGameStats(int id)
        {
            var userStatsSingle = _context.StatsSingleplayer.Where(s => s.Id == id && s.GameMode == "SinglePlayer").First();
            var userStatsMulti = _context.StatsMultiplayer.Where(s => s.Id == id && s.GameMode == "MultiPlayer").First();

            UserStatsModel model = new UserStatsModel
            {
                TotalMonstersKilled = userStatsSingle.MonstersKilled,

                TotalGamesPlayed = userStatsSingle.GamesPlayed + userStatsMulti.GamesPlayed,

                SingleplayerHighestMonstersKilled = userStatsSingle.HighestKills,

                SingleplayerGamesPlayed = userStatsSingle.GamesPlayed,

                SingleplayerHighestRound = userStatsSingle.HighestRound,

                SingleplayerHighestScore = userStatsSingle.BestScore,

                MultiplayerWins = userStatsMulti.Wins,

                MultiplayerDefeats = userStatsMulti.Defeats,

                MultiplayerWinRate = userStatsMulti.WinRate,

                MultiplayerGamesPlayed = userStatsMulti.GamesPlayed,

            };

            return model;
        }

    }
}
