﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class SifreDegistirme
    {

        [Required]
        [DisplayName("Eski Şifre")]
        public string OldPassword { get; set; }

        [Required]
        [DisplayName("Yeni Şifre")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Şifreniz En Az 5 karakter olmalıdır ..")]
        public string NewPassword { get; set; }

        [Required]
        [DisplayName("Tekrar Şifre")]
        [Compare("NewPassword",ErrorMessage ="Şifreler Uyuşmamaktadır ..")]
        public string ConNewPassword { get; set; }
      
    }
}