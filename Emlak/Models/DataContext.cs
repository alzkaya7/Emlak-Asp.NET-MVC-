using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Emlak.Models
{
    public class DataContext:DbContext
    {
        //Tablolarımı oluşturmak için Dbset ten yardım aldık ..Modellerimizi yerleştirdik ..

        public DataContext():base("dataConnection") // WebConfigde yeni bir veritabanı oluşturduk .. Onu atadık ..
        {
            Database.SetInitializer(new DataInıtializer());
        }
        public DbSet<Sehir> Sehirs { get; set; }
        public DbSet<Semt> Semts { get; set; }
        public DbSet<Mahalle> Mahalles { get; set; }
        public DbSet<Durum> Durums { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Ilan> Ilans { get; set; }
        public DbSet<Resim> Resims { get; set; }
        
    }
}