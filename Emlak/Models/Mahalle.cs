using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Mahalle
    {
        public int MahalleId { get; set; }
        [Required]
        public string MahalleAd { get; set; }
        public int SemtId { get; set; } // Her mahallenin birtane semti olduğu için bunu yazdım ..
        public virtual Semt Semt { get; set; } //Mahallelerin ait olduğu semtin bilgisini getirmek için bunu yazdım ..

    }
}