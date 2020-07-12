using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stajbul.Models
{
    public class jobPosting
    {
        [Required]
        public int id{ get; set; }
        [Required]
        [DisplayName("Başlık")]
        public string title{ get; set; }
        [DisplayName("Sektör")]
        public string sector { get; set; }
        [DisplayName("Açıklama")]
        public string description { get; set; }
        public List<talentsofJobs> talentsofJobs { get; set; }
        public List<jobApplications> jobApplications { get; set; }
    }
}
