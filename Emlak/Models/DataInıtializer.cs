using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Emlak.Models
{
    public class DataInıtializer : DropCreateDatabaseIfModelChanges<DataContext>

    {
        //Bu class ikide bir veritabanına gidip bilgi girmekle uğraşmayalım diye yazılmıştır .

        protected override void Seed(DataContext context)
        {
            //var sehir = new List<Sehir>()
            //{
            //    new Sehir() {SehirAd="İSTANBUL" },
            //    new Sehir() {SehirAd="ANKARA" },
            //    new Sehir() {SehirAd="İZMİR" }
            //};
            //foreach (var item in sehir)
            //{
            //    context.Sehirs.Add(item);
            //}
            //context.SaveChanges();
            //var semt = new List<Semt>()
            //{

            //    new Semt() {SemtAd="KAĞITHANE",SehirId=34 },
            //    new Semt() {SemtAd="KEÇİÖREN",SehirId=6 },
            //    new Semt() {SemtAd="BORNOVA",SehirId=35 },
            //};
            //foreach (var item in semt)
            //{
            //    context.Semts.Add(item);
            //}
            //context.SaveChanges();

            //var mahalle = new List<Mahalle>()
            //{
            //    new Mahalle() {MahalleAd="ÇAĞLAYAN",SemtId=1 },
            //    new Mahalle() {MahalleAd="İZZETPAŞA",SemtId=2 },
            //    new Mahalle() {MahalleAd="TALATPAŞA",SemtId=3 },
            //};
            //foreach (var item in mahalle)
            //{
            //    context.Mahalles.Add(item);
            //}
            //context.SaveChanges();

            //var durum = new List<Durum>()
            //{
            //    new Durum() {DurumAd="KIRALIK" },
            //    new Durum() {DurumAd="SATILIK" }
            //};
            //foreach (var item in durum)
            //{
            //    context.Durums.Add(item);
            //}
            //context.SaveChanges();

            //var tip = new List<Tip>()
            //{
            //    new Tip() {TipAd="EV",DurumId=1 },
            //     new Tip() {TipAd="DÜKKAN",DurumId=1 },
            //     new Tip() {TipAd="EV",DurumId=2 },
            //     new Tip() {TipAd="DÜKKAN",DurumId=2 },
                
            //};
            //foreach (var item in tip)
            //{
            //    context.Tips.Add(item);
            //}
            //context.SaveChanges();
            //var ilan = new List<Ilan>()
            //{
            //    new Ilan() {Açıklama="ev güzel",Adres="şan sokak",OdaSayisi=5,BanyoSayisi=2,Kredi=true,Fiyat=2500,MahalleId=1,SemtId=1,SehirId=1,DurumId=1,TipId=1,Alan=250,Telefon="2122121212",Kat="2.kat",UserName="alican" },
            //     new Ilan() {Açıklama="ev güzel 1",Adres="can sokak",OdaSayisi=3,BanyoSayisi=1,Kredi=true,Fiyat=500000,MahalleId=2,SemtId=2,SehirId=2,DurumId=2,TipId=4,Alan=150,Telefon="2122121212",Kat="5.kat",UserName="ahmetcan" },
            //};
            //foreach (var item in ilan)
            //{
            //    context.Ilans.Add(item);
            //}
            //context.SaveChanges();
            //var resim = new List<Resim>()
            //{
            //    new Resim() {ResimAd="1.jpg",IlanId=1 },
            //    new Resim() {ResimAd="2.jpg",IlanId=1 },
            //    new Resim() {ResimAd="3.jpg",IlanId=2 },
            //};
            //foreach (var item in resim)
            //{
            //    context.Resims.Add(item);
            //}
            //context.SaveChanges();



            //base.Seed(context);
        }
    }
}