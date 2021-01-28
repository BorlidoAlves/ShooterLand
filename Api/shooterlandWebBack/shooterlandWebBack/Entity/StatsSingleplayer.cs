using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Entity
{
    public class StatsSingleplayer
    {
        public int Id { get; set; }
        public int MonstersKilled { get; set; }
        public int BestScore { get; set; }
        public int HighestRound { get; set; }
        public string GameMode { get; set; }
        public int GamesPlayed { get; set; }
        public int HighestKills { get; set; }

        [Key]
        public int IdStat { get; set; } 

    }
}
