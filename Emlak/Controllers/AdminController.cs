using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Emlak.Controllers
{

    [Authorize(Roles ="admin")] //Controller üstüne yazdım , sisteme giriş yapamadan ilan controllera erişemez ..
                                // Admin rolü dışıdnaki hiç kimse ereişemez ..Controllera ait hiçbir methoda erişemez .. 
    public class AdminController : Controller
    {
        DataContext db=new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IlanListesi()
        {

            //İlanları listele tip ve mahalleyi kullaranak ..Çünkü şehir ve semt durum bilgileri mahalle ve tip ile bağlanatılı .. 
            var ilan=db.Ilans.Include(i=>i.Mahalle).Include(i=>i.Tip).ToList();
            return View(ilan);

        }
    }
}