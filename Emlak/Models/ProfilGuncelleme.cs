using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class ProfilGuncelleme
    {
        public string id { get; set; }
        [Required]
        [DisplayName("Adı")]

        public string Name { get; set; }
        [Required]
        [DisplayName("Soyadı")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage ="Geçerli Bir E-Mail adresi giriniz ..")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        

    }
}