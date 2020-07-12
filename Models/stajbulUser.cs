using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stajbul.Models
{
    public class stajbulUser
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Kullanıcı adı zorunludur.")]
        [DisplayName("Kullanıcı Adı")]
        public string username { get; set; }

        [Required(ErrorMessage ="Şifre zorunludur.")]
        [DisplayName("Şifre")]
        public string password { get; set; }
        [DisplayName("Ad-Soyad")]
        public string fullname { get; set; }
        [DisplayName("Doğum Tarihi")]
        public DateTime birthdate { get; set; }
        [DisplayName("Üniversite")]
        public string school { get; set; }
        [DisplayName("Bölüm")]
        public string department { get; set; }
        [DisplayName("Admin")]
        public bool isAdmin { get; set; }
        public List<talents> talents { get; set; }
        public List<jobApplications> jobApplications { get; set; }

        public stajbulUser()
        {
            isAdmin = false;
        }

    }
}
