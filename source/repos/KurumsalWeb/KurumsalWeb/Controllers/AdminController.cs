using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System.Web.Helpers;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext db=new KurumsalDBContext(); //veritabanı nesnemizini instance

        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.YorumSay = db.Yorum.Count();
            ViewBag.OneCikanlarSay = db.OneCikanlar.Count();
            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count(); //onaylanmayan yorumların sayısını çekmiş olduk. verilerimizi Controller dan View kısmına aktarmak için kullanırız.
            var sorgu = db.Kategori.ToList();
            
            return View(sorgu);
        }
        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

      
        public ActionResult Login(Admin admin) //admin modeli
        {
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();//veritabanından bakacak modelden gelen epostayla eşleşen bir kayıt var mı.
            if (login.Eposta==admin.Eposta && login.Sifre==Crypto.Hash(admin.Sifre,"MD5"))
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış ";
            return View(admin);
        }

        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
            
        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            var mail = db.Admin.Where(x => x.Eposta == eposta).SingleOrDefault();
            if (mail!=null)
            {
                Random rnd=new Random(); //rastgele sayı üretir
                int yeniSifre = rnd.Next(); //yeni şifreyi random sayıyla oluşturuyoruz
                Admin admin=new Admin(); //admin den instance oluşturduk.
                mail.Sifre = Crypto.Hash(Convert.ToString(yeniSifre), "MD5"); //sorgudan gelen mail adresinin şifre alanına yeni şifryi stringe çevirip sonrada cryptolu bir şekkilde yoluyoruz.
                db.SaveChanges();//veritabanına kaydet.
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true; //güvenli bağlantı oluşturulsun.
                WebMail.UserName = "berfuceren96@gmail.com";
                WebMail.Password = "Ceren14545561.";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Admin Panel GirişŞifreniz","Şifreniz :"+ yeniSifre); //kime gitmesini istiyoruz.
                ViewBag.Uyari = "Mesajınız başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu. Tekrar deneyiniz";
            }
            return View();
          
        }

        public ActionResult YazarOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarOl(Yazar yazar)
        {
            yazar.Aktif = true;
            yazar.Onay = true;
            db.Yazar.Add(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admin admin,string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }

            
            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(int id, Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {

                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }

            return View(admin);
        }

        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            if (a!=null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(a);
        }
    }
}