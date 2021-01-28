using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Models
{
    public class UpdatePassword
    {
        [Required]
        public string Password { get; set; }
    }
}
