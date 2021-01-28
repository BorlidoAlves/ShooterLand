using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    public class UserStatsModel
    {
        public int TotalMonstersKilled { get; set; }

        public int TotalGamesPlayed { get; set; }

        public int SingleplayerHighestMonstersKilled { get; set; }
        
        public int SingleplayerGamesPlayed { get; set; }

        public int SingleplayerHighestRound { get; set; }

        public int SingleplayerHighestScore { get; set; }

        public int MultiplayerWins { get; set; }

        public int MultiplayerDefeats { get; set; }

        public int MultiplayerWinRate { get; set; }

        public int MultiplayerGamesPlayed { get; set; }

    }
}
