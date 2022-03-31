using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Sehir
    {
        public int SehirId { get; set; }
        [Required]
        public string SehirAd { get; set; }

        public List<Semt> Semts { get; set; } //Her şehrin birden fazla semti olabilir  ..Bire çok ilişkiden ötürü Şehrin semtlerini listelemek için bunu yazdım ..
    }
}