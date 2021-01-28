using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Entity
{
    public class StatsMultiplayer
    {
        public int Id { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int GamesPlayed { get; set; }
        public int WinRate { get; set; }
        public string GameMode { get; set; }
        [Key]
        public int IdStatMulti { get; set; }
    }
}
