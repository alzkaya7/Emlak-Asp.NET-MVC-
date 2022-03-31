using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Tip
    {
        public int TipId { get; set; }

        [Required]
        public string TipAd { get; set; }
        public int DurumId { get; set; } // Her tipin bir durumu olabilir ..
        public virtual Durum Durum { get; set; } //Her tipin bir durumu olabilceği için bilgilere ihityacım var ve bunun için Durumların bilgisine ulaşmak için bunu yazdım ..


    }
}