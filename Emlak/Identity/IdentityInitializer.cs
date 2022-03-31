using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace Emlak.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext> // Eğer bir database yoksa database oluştur ..Oluşturduğumuz IdentityDtaContext sınıfını kulannadık ..
    {
        protected override void Seed(IdentityDataContext context)
        {

            //Admin Rolünü oluşturuyoruz ..
            if (!context.Roles.Any(i => i.Name == "admin")) //burada admin rolü yoksa bir rol oluştur ve adı Admin olsun dedim ..
            {
                var store = new RoleStore<ApplicationRole>(context); //oluşturduğumuz admin rolünü Rolestore ekledik , yani admin rolümüz hazır ..
                var manager = new RoleManager<ApplicationRole>(store); //burada manager oluşturduk ..
                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" };
                manager.Create(role); //bu şekilde admin rolünü oluşturduk ..
            }


            //user rolü oluşturduk .. Bu şekilde isteidğimiz kadar rol oluştıurbaliriz ..
            if (!context.Roles.Any(i => i.Name == "user")) //burada user rolü yoksa bir rol oluştur ve adı user olsun dedim ..
            {
                var store = new RoleStore<ApplicationRole>(context); //oluşturduğumuz admin rolünü Rolestore ekledik , yani user rolümüz hazır ..
                var manager = new RoleManager<ApplicationRole>(store); //burada manager oluşturduk ..
                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role); //bu şekilde user rolünü oluşturduk ..
            }

            //böyle user oluşturduk rol değil  .. ama rol oluşturmakla aynı mantık burda var ..sadece rol yerine user geliyor ..

            if (!context.Users.Any(i => i.Name == "AliÖzkaya"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Ali", Surname = "Özkaya", UserName = "alzkaya7", Email = "alzkaya7@gmail.com" };
                manager.Create(user, "alzkaya7");
                manager.AddToRole(user.Id, "admin"); //Ali özkaya hem admin hem user olabilcek ..
                manager.AddToRole(user.Id, "user");

            }


            if (!context.Users.Any(i => i.Name == "NurşahÖzkaya"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Nurşah", Surname = "Özkaya", UserName = "nursevin", Email = "nursevin@gmail.com" };
                manager.Create(user, "75897589");

                manager.AddToRole(user.Id, "user"); //Nur sevin sadece user olucak ona admin rolü vermedik ..

            }
            base.Seed(context);



        }

    }
}

