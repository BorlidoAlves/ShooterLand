using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using shooterlandWebBack.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    /// <summary>
    /// Model when achievement does the update
    /// </summary>
    public class AchievUpdateModel
    {
        public string Description { get; set; }

        public byte[] Medal { get; set; }

        public AchievementType Type { get; set; }

        public int Value { get; set; }
    }
}
