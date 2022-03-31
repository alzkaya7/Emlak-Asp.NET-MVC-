using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emlak.Models;

namespace Emlak.Controllers
{
    public class IlanController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Ilan
        public ActionResult Index()
        {
            //ilan görütülerken her ilanın gelmesini sitemiyorum , sadece  ilgili kullanıcının veridği ilanların listelenmesini istiyorum ..
            var username = User.Identity.Name; //Sistemde olan kullanıcıyı buluyoruz ..

            var ilans = db.Ilans.Where(i=>i.UserName==username).Include(i => i.Mahalle).Include(i => i.Tip);
            return View(ilans.ToList());
        }
        public  List<Sehir> SehirGetir()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;


        }


        //Dropdownda Semt Listeleme Şehre göre
        public ActionResult SemtGetir(int SehirId) //Şehir ID sine göre semtleri getirceği için SehirId çağırdık ..
        {
            List<Semt> semtler = db.Semts.Where(x=>x.SehirId==SehirId).ToList(); //Seçtiğim şehrin semtleri gelsin ...Bütün semtler gelmesin ..
            ViewBag.semtlistesi = new SelectList(semtler, "SemtId", "SemtAd"); //Dropdown liste eklemek için bu işlermleri yaptık ..

            return PartialView("SemtPartial");


        }

        //Dropdownda Mahalle Listeleme semte göre
        public ActionResult MahalleGetir(int SemtId)
        {
            List<Mahalle> mahallelist=db.Mahalles.Where(x=>x.SemtId==SemtId).ToList(); //seçtiğim semte göre mahalleler gelsin ..
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

        public ActionResult Images(int id)
        {
            var ilan = db.Ilans.Where(i => i.IlanId == id).ToList();//İlanın ıd sini getirmiş olduk ..
            var resml=db.Resims.Where(i => i.IlanId == id).ToList();//Resmin ilan numarasını bularak resim ve ilan bağlantısını kurmuş olduk..    
            ViewBag.rsml = resml;
            ViewBag.ilan= ilan;
            return View();

        }

        [HttpPost]
        public ActionResult Images(int id,HttpPostedFileBase file)
        {
            string path = Path.Combine("/Content/images/" + file.FileName);// Resim yolunu  resmin nereye gideceğini aldık ..
            file.SaveAs(Server.MapPath(path)); //Resmin nereye kaydolcağını belirledik  ve yolunu bağladık ....
            Resim rsm=new Resim(); //Resim modelinden nesne üretiyoruz ..Property leri kullanmak için ..
            rsm.ResimAd = file.FileName.ToString(); //Bu şekilde bu modelin resmi budur ve resim adı budur dedim ..
            rsm.IlanId = id; //Bu resim ile ilanı birbirne bağladık ..
            db.Resims.Add(rsm); // Resimler listesine belrilediğimiz reism yolunu ekliyoruz .. String olarak veritabanaına kaydediyoruz ..
            db.SaveChanges(); //Değişiklikleri kaydet ..
            return RedirectToAction("Index");

        }

        // GET: Ilan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // GET: Ilan/Create
        public ActionResult Create()
        {
           
            ViewBag.durumlist = new SelectList(DurumGetir() , "DurumId", "DurumAd");
            ViewBag.sehirlist=new SelectList(SehirGetir(),"SehirId","SehirAd");
            ViewBag.MahalleId = new SelectList(db.Mahalles, "MahalleId", "MahalleAd");
            ViewBag.TipId = new SelectList(db.Tips, "TipId", "TipAd");
            return View();
        }

        // POST: Ilan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IlanId,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirId,SemtId,DurumId,MahalleId,TipId")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                ilan.UserName = User.Identity.Name;
                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd"); //Şehirleri listelemiş oluytoruz. ..Partialviewa girt ..
            ViewBag.MahalleId = new SelectList(db.Mahalles, "MahalleId", "MahalleAd", ilan.MahalleId);
            ViewBag.TipId = new SelectList(db.Tips, "TipId", "TipAd", ilan.TipId);
            return View(ilan);
        }

        // GET: Ilan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd",ilan.SemtId);

            ViewBag.MahalleId = new SelectList(db.Mahalles, "MahalleId", "MahalleAd", ilan.MahalleId);
            ViewBag.TipId = new SelectList(db.Tips, "TipId", "TipAd", ilan.TipId);
            return View(ilan);
        }

        // POST: Ilan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IlanId,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirId,SemtId,DurumId,MahalleId,TipId")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
           
            return View(ilan);
        }


        // GET: Ilan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            db.Ilans.Remove(ilan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}