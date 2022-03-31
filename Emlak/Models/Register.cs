using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyadı")]
        public string Surname { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage ="Geçersiz E-mail Adresi ...")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="Şifreler Aynı Olmalıdır ...")]
        [DisplayName("Şifre Tekrar")]

        public string RePassword { get; set; }


    }
}