using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stajbul.Models
{
    public class talents
    {
        [Required]        
        public int id { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Yetenek")]
        public string name { get; set; }

    }
}
