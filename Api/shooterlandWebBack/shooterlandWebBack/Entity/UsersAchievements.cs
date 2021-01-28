using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Entity
{
    /// <summary>
    /// Entity to deal whith the Users Achievements 
    /// </summary>
    
    public class UsersAchievements
    {
        public int IdUser { get; set; }
        [Key]
        public int IdAchievement { get; set; }
    }
}
