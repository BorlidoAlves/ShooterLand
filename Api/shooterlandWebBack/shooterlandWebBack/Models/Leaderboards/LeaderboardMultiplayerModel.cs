using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models.Leaderboards
{
    public class LeaderboardMultiplayerModel
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public float WinRate { get; set; } 
    }
}
