using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    public class LeaderboardKillsModel
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public int Kills { get; set; }
    }
}
