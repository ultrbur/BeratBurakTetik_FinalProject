using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace stajbul.Models
{
    public class talentsofJobs
    {
        public int id { get; set; }
        public int talentID { get; set; }
        [DisplayName("Yetenek")]
        public talents talent { get; set; }
        public int jobPostingID { get; set; }
        [DisplayName("İlan")]
        public jobPosting jobPosting { get; set; }
    }
}
