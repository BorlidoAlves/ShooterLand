using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    public class StatUpdateModel
    {
        public int MonstersKilled { get; set; }
        public int Score { get; set; }
        public int Round { get; set; }
        public string GameMode { get; set; }
    }
}
