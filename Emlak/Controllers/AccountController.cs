using Emlak.Identity;
using Emlak.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //UserManager sınıfı Uygulama üzerinde A'dan Z'ye Kullanıcı yönetimini gerçekleştirmemizi sağlar ..
        private UserManager<ApplicationUser> UserManager;  //Bu sınıf hangi kullanıcı türünü yöneteceğini bilmek ister o yüzden ApplicationUser sınıfını kullandık .. 
        private RoleManager<ApplicationRole> RoleManager; //Hangi rolün belirli yetkilerle hangi işlemleri yapmakla sınırlandırılması olayını sağlar ...
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext()); //Tüm kullanıcıların yönetildiği sınıf UserStore ..
            UserManager = new UserManager<ApplicationUser>(userStore); //user manager ile userstore ilişkilendirdik .. Kullanıcıları tanımlamış ve yönetmiş olduk ...
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext()); // tüm rolleri bu storeda topluyoruz ..
            RoleManager=new RoleManager<ApplicationRole>(roleStore); //role manager ile rolestore ilişkililendirildi ..


        }
        //şifre değiştrme işlemleri ..
        public ActionResult SifreDegistir()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SifreDegistir(SifreDegistirme model)
        {
            if (ModelState.IsValid) //Kullanıcı zorunlu alanları doldurmuş mu ??
            {
                //userManager ın  ChangePassword özelliğini kullandık ..
                var user=UserManager.ChangePassword(User.Identity.GetUserId(),model.OldPassword,model.NewPassword); //Kullanııcyı ID ye göre bul ,eski şifreyi yeni şifreyle değiştir ..
                return View("Update");
            }
            return View(model);
        }

        //Profil Güncelleme Kısmı
        public ActionResult Profil()
        {
            var id=HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId(); //Bu bize sisteme giriş yapan kullanıcının ID sini verecektir ..
            var user=UserManager.FindById(id); //Bu şekilde kullanıcıyı bulmuş oluyoruz ..
            var data = new ProfilGuncelleme()
            {
                //Bu şekilde kullanıcı bilgilerini gerekli alanlara getirmiş oluyoruz ..
                id = user.Id, // sol taraf applicationuser ve ıdenttiyuser ortaklı verilerken ,  diğer taraf ise profilden gelen veriler .
                Name = user.Name,
                Username = user.UserName,
                Surname = user.Surname,
                Email = user.Email,



            };

            return View(data);

        }

        [HttpPost]
        public ActionResult Profil(ProfilGuncelleme model)
        {
            var user = UserManager.FindById(model.id); //Kullanıcıyı bulduk ..
            user.Surname = model.Surname;
            user.Name= model.Name;
            user.UserName = model.Username;
            user.Email = model.Email;
            UserManager.Update(user);



            return View("Update");
        
        }

            public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Login model,string Returnurl) // oluşturduğumuz Login modelini burada kullanıyoruz ..
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.UserName, model.Password);//Kullanıcıyı bulma işlemini yapıyoruz ..
                if (user != null) //Kullanıcı kullanıcı adı ve şifre bilgisi girdikten sonra bulma işlemini yapcak ve eğer bir kullanıcı  gelmişse bunu bilgiyle devam edicek ..Yani kullanıcı bilgisi boş gelmemişsse
                {
                    var authManager=HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie"); //Burda oluşturğumuz user ı bir Cookie ininiçine atıyoruz ..
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe; //Session devamlı açık yada kapalı olmasını sağlıyoruz ..
                    authManager.SignIn(authProperties, identityclaims); //Bu şekilde kullanıcıyı sisteme dahil ediyoruz ..
                    if(!string.IsNullOrEmpty(Returnurl)) // Kullanıcı giriş yapmadan izinli olmayan sayfalara gitmeden Login sayfasına yönlendirilir .
                    {

                        return Redirect(Returnurl);
                    }
                    return RedirectToAction("Index","Home"); //Giriş yaptıktan sonrada Home içindeki Index Sayfasına yönlendirilsin yani Anasayfaya ..
                }
                else
                {

                    ModelState.AddModelError("Kullanıcı Giriş Hatası ..", "Böyle Bir Kullanıcı Bulunamadı ..");
                }
            }


            return View(model);
        }

        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication; //önce bi cookieleri çağırıp silelim  ..
            authManager.SignOut(); // Kullanıcın sistemden çıkış yapmasını sağlıyoruz. .
            return RedirectToAction("Index" ,"Home"); //view kullanmadan LogOut İşlemini sonlandırıyoruz ve Anasayfaya yönlendiriyoruz .. ..
            

         }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register model) // Register işlemi için Register modelinden yararlanacağım o yüzden Register türünden değişken tanımladık ..
        {
            if (ModelState.IsValid) //Yani kullanıcı zorunlu alanları doldurmuşssa ,kayıt işlemlerini yerine getirsin ..
            {
                var user = new ApplicationUser();
                
                user.Name=model.Name;

                user.UserName = model.Username;

                user.Surname=model.Surname;

                user.Email = model.Email;

                var result=UserManager.Create(user,model.Password); //Yeni bir user oluşturur .. İlk parameterede yukardaki user geldi , diğer parametre ise modelin şifresi yani ..

                if (result.Succeeded) //eğer sonuç başarılı ise yani kayıt işlemi gerçekleşmiş ise 
                {
                    if (RoleManager.RoleExists("user")) // Kayıt olan kullanıcının rolü kontrol edildi ..
                                                        // Bizim oluşturduğumuz modelin içinde user varsa bunu yeni kayıt olan kullanıcıya verebiliriz..
                                                        // Sisteme kaydolan her kullanıcı "User olucak"..Diğer rol ise Admin rolüdür ..
                    {
                        UserManager.AddToRole(user.Id, "user"); //Bu rolü yeni kayıt olan kullanıcıya verdik ..

                    }
                    return RedirectToAction("Login", "Account"); //Yani Kullanıcı kayıt olmuşssa Account Controllerın Login sayfasına gitsin .. (Action adı ,Controller Adı ..)

                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı Oluşturma Hatası .."); //Kayıt oluşturma esnasında bir hata varsa bu mesajları hata olarak versin ..
                }


            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}