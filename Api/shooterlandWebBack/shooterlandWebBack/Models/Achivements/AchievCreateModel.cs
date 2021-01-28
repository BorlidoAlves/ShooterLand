using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using shooterlandWebBack.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    /// <summary>
    /// Model of when achievement is created
    /// </summary>
    public class AchievCreateModel
    {
        [Required]
        public string Description { get; set; }
    
        public string Medal { get; set; } 
        
        [NotMapped]
        public IFormFile MedalFile { get; set; }

        [Required]
        public AchievementType Type { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
