using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Semt
    {
        public int SemtId { get; set; }
        [Required]
        public string SemtAd { get; set; }
        public int SehirId { get; set; }

        public virtual Sehir Sehir { get; set; }  //Semtlerin bağlı olduğu şehirlerin bilgisine rahatlıkla ulaşbilmek için bu yazıldı .
        
        public List<Mahalle> Mahalles { get; set; } //Bir semtin birden fazla mahallesi olabilir ..
    }
}