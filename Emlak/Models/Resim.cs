using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class Resim
    {
        public int ResimId { get; set; }
        public string ResimAd { get; set; }
        public int IlanId { get; set; } // Bir resim bir ilana ait olabilir .. 
        public virtual Ilan Ilan { get; set; } //Resmin ait olduğu ilanın bilgilerini getirmek için bu property yazıldı . 


    }
}