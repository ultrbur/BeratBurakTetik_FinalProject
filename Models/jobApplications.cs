using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace stajbul.Models
{
    public class jobApplications
    {
        public int id { get; set; }
        public int stajbulUserID { get; set; }
        [DisplayName("Kullanıcı")]
        public stajbulUser stajbulUser { get; set; }
        public int jobPostingID { get; set; }
        [DisplayName("İlan")]
        public jobPosting jobPosting { get; set; }
    }
}
