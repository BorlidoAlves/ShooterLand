using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using shooterlandWebBack.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace shooterlandWebBack.Entity
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AchievementType
    {
        Score = 0,
        Kills = 1,
        Rounds = 2
    } 
    /// <summary>
    /// Entity of Achievement
    /// </summary>
    public class Achievement
    {
        [Key]
        public int IdAchievement { get; set; }

        public string Description { get; set; }
    
        public string Medal { get; set; }
        
        public AchievementType Type { get; set; }

        public int Value { get; set; }
    }
}
