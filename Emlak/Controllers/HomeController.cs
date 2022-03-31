using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emlak.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;


namespace Emlak.Controllers
{
    public class HomeController : Controller
    {

        DataContext db=new DataContext();
        // GET: Home
        public ActionResult Index(int sayi=1) //Başlayacağı sayfa sayısı  .. yani  1 ..
        {

            //resimleri veritabanından controllera taşıma işlemini viewbag ile yaptık..İLk önce listelettik ve daha sonra viewbag içine taşıdık...
            var imgs =db.Resims.ToList(); 
            ViewBag.imgs = imgs; 

            //mahalle ve tip tablolarına ihtiyacım olacağı için bunlarıda dahil etmem gerekiyor ve ben bunu Include ile yapıyorum ..
            var ilan = db.Ilans.Include(m => m.Mahalle).Include(e => e.Tip);

            return View(ilan.ToList().ToPagedList(sayi,3));
        }

        public ActionResult DurumList(int id) //dışardan ıd alıyorum çünkü durum ıd ye ggöre işlme yapacağım ..

        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var ilan=db.Ilans.Where(i=>i.DurumId == id).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
            return View(ilan);

        }

        public ActionResult MenuFiltre(int id)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var filtre=db.Ilans.Where(i=>i.TipId==id).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
            return View(filtre);
        }

        public PartialViewResult PartialFiltre()
        {
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            return PartialView();

        }

        //filtreleme işlemi için bana 7 parametere gerekliydi onları tanımladım ve ılan prop ları ile eşleştiridim ..
        public ActionResult Filtre(int min,int max,int sehirid,int mahalleid,int semtid,int durumid,int tipid)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var filtre = db.Ilans.Where(i => i.Fiyat >= min && i.Fiyat <= max
              && i.DurumId == durumid
              && i.TipId == tipid
              && i.MahalleId == mahalleid
              && i.SehirId == sehirid
              && i.SemtId == semtid).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
            return View(filtre);




        }



        //Dropdownda Semt Listeleme Şehre göre
        public ActionResult SemtGetir(int SehirId) //Şehir ID sine göre semtleri getirceği için SehirId çağırdık ..
        {
            List<Semt> semtler = db.Semts.Where(x => x.SehirId == SehirId).ToList(); //Seçtiğim şehrin semtleri gelsin ...Bütün semtler gelmesin ..
            ViewBag.semtlistesi = new SelectList(semtler, "SemtId", "SemtAd"); //Dropdown liste eklemek için bu işlermleri yaptık ..

            return PartialView("SemtPartial");


        }

        //Dropdownda Mahalle Listeleme semte göre
        public ActionResult MahalleGetir(int SemtId)
        {
            List<Mahalle> mahallelist = db.Mahalles.Where(x => x.SemtId == SemtId).ToList(); //seçtiğim semte göre mahalleler gelsin ..
            ViewBag.mahallelistesi = new SelectList(mahallelist, "MahalleId", "MahalleAd");

            return PartialView("MahallePartial");
        }

        public List<Durum> DurumGetir()
        {
            List<Durum> durumlar = db.Durums.ToList(); //seçtiğim semte göre mahalleler gelsin ..
            return durumlar;
        }

        public ActionResult TipGetir(int DurumId)
        {
            List<Tip> tiplist = db.Tips.Where(x => x.DurumId == DurumId).ToList(); //seçtiğim duruma göre tipler gelsin ..
            ViewBag.tiplistesi = new SelectList(tiplist, "TipId", "TipAd");
            return PartialView("TipPartial");
        }



        public ActionResult Search(string q)
        {
            var imgs = db.Resims.ToList();
            ViewBag.imgs = imgs;
            var ara = db.Ilans.Include(m => m.Mahalle).Include(e => e.Tip); // ilanın semt şehir durum bilgilerine mahalle ve tip bilgisiyle ulaştığım için bu yazıldı ..
            if (!string.IsNullOrEmpty(q)) //eğer q nun içi boş yada null değerse dmekki q ya değer gelmiş ..
            {
                ara = ara.Where( i => i.Açıklama.Contains(q) || i.Mahalle.MahalleAd.Contains(q) || i.Tip.TipAd.Contains(q)); //Bu şekilde q nun değerine göre açıklama mahalle tip isminde arama yapar ..


            }

            return View(ara.ToList());
        }


        public ActionResult Detay(int id)
        {
            var ilan = db.Ilans.Where(i => i.IlanId == id).Include(m => m.Mahalle).Include(e => e.Tip).FirstOrDefault();
            var imgs = db.Resims.Where(i => i.IlanId == id).ToList();
            ViewBag.imgs=imgs;
            return View(ilan);
        }
         public PartialViewResult Slider()
        {

            var ilan=db.Ilans.ToList().Take(5); // Take içine kaç yazarsam o kadar resim gelmesini istiyorum slayda ..
            var imgs = db.Resims.ToList(); //viewbag içine resimler listesini gönderdik ve view tarafında foreach ile veri çektik ..
            ViewBag.imgs=imgs;
            return PartialView(ilan);
        }
        public List<Sehir> SehirGetir()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;
        }

     
      
    }
}