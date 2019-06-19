using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{

    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        private const string KULLANICI_ID = "kullaniciId";

        // GET: Home
        /*[Route("")]
        [Route("S")]*/

        public ActionResult Index()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.OneCikanlar = db.OneCikanlar.ToList().OrderByDescending(x => x.OneCikanId);

            return View();
        }

        public ActionResult UyeOl()
        {

            return View();
        }

        [HttpPost]

        public ActionResult UyeOl(Kullanici kullanici)
        {

            //kullanici.Yazar = false;
            //kullanici.Onay = true;
            //kullanici.Aktif = true;
            kullanici.Sifre = Crypto.Hash(kullanici.Sifre, "MD5");
            db.Kullanici.Add(kullanici);
            db.SaveChanges();
            Session["kullaniciId"] = kullanici.KullaniciId;
            Session["kullaniciadi"] = kullanici.KullaniciAdi;
            return RedirectToAction("Login");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]


        public ActionResult Login(Kullanici kullanici) //kullanıcı modeli geliyor
        {
            var login = db.Kullanici.Where(x => x.Eposta == kullanici.Eposta)
                .SingleOrDefault(); //veritabanından bakacak modelden gelen epostayla eşleşen bir kayıt var mı.
            if (login.Eposta == kullanici.Eposta && login.Sifre == Crypto.Hash(kullanici.Sifre, "MD5"))
            {
                Session["kullaniciId"] = login.KullaniciId;
                Session["eposta"] = login.Eposta;
                //Session["yetki"] = login.Yetki;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Uyari = "E-posta yada şifrenizi kontrol ediniz ";
                return View(kullanici);
            }


        }

        public ActionResult Logout()
        {
            Session["kullaniciId"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            var mail = db.Kullanici.Where(x => x.Eposta == eposta).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random(); //rastgele sayı üretir
                int yeniSifre = rnd.Next(); //yeni şifreyi random sayıyla oluşturuyoruz
                Kullanici kullanici = new Kullanici(); //admin den instance oluşturduk.
                mail.Sifre =
                    Crypto.Hash(Convert.ToString(yeniSifre),
                        "MD5"); //sorgudan gelen mail adresinin şifre alanına yeni şifryi stringe çevirip sonrada cryptolu bir şekkilde yoluyoruz.
                db.SaveChanges(); //veritabanına kaydet.
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true; //güvenli bağlantı oluşturulsun.
                WebMail.UserName = "berfuceren96@gmail.com";
                WebMail.Password = "Ceren14545561.";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Admin Panel GirişŞifreniz", "Şifreniz :" + yeniSifre); //kime gitmesini istiyoruz.
                ViewBag.Uyari = "Mesajınız başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu. Tekrar deneyiniz";
            }

            return View();

        }

        public ActionResult SliderPartical()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.SliderId));
        }

        public ActionResult OneCikanlarPartical()
        {
            return View(db.Blog.Include("Kategori").ToList().OrderBy(x => x.Kategori.KategoriAd));
        }

        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {


            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }

        [Route("OneCikanlar")]
        public ActionResult OneCikanlar()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.OneCikanlar.ToList().OrderByDescending(x => x.OneCikanId));
        }

        [Route("iletisim")]
        public ActionResult Iletisim()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null,
            string mesaj = null) //gelen parametreler
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true; //güvenli bağlantı oluşturulsun.
                WebMail.UserName = "berfuceren96@gmail.com";
                WebMail.Password = "Ceren14545561.";
                WebMail.SmtpPort = 587;
                WebMail.Send("berfuceren96@gmail.com", konu, email + "-" + mesaj); //kime gitmesini istiyoruz.
                ViewBag.Uyari = "Mesajınız başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu. Tekrar deneyiniz";
            }

            return View();
        }

        [Route("BlogPost")]
        public ActionResult Blog(int Sayfa = 1)
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 5));
        }

        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).Where(x => x.Kategori.KategoriId == id)
                .ToPagedList(Sayfa, 5);
            return View(b);
        }

        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            if (Session[KULLANICI_ID] != null && (int)Session[KULLANICI_ID] > 0)
                ViewBag.IsMember = true;
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            Blog blog = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.BlogId == id).FirstOrDefault();
            return View(blog);
        }

        public JsonResult YorumYap(string icerik, int blogid)
        {
            var kullanıcıId = Session[KULLANICI_ID];
            if (Session[KULLANICI_ID] == null || (int)Session[KULLANICI_ID] <1)
            {
                return Json("Üzgünüz. Bir hata oluştur. Lütfen tekrar deneyiniz.", JsonRequestBehavior.AllowGet);
            }
            var user = db.Kullanici.Find(Session[KULLANICI_ID]);
            
            if (icerik == null)
            {
                return Json("Lütfen önce bir içerik giriniz.", JsonRequestBehavior.AllowGet);
            }

            var yorumToAdd = new Yorum
            {
                AdSoyad = user.Adi, Eposta = user.Eposta, Icerik = icerik, BlogId = blogid,
                KullaniciId = (int) Session[KULLANICI_ID],
                EklenmeTarihi =  DateTime.Now
            };
            yorumToAdd.Onay = yorumToAdd.IsEligible;

            if (!yorumToAdd.IsEligible)
            {
                return Json("Hatalı veri girişi yaptınız. Lütfen tekrar deneyiniz.", JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(yorumToAdd);
            db.SaveChanges();
           
            return Json(true, JsonRequestBehavior.AllowGet); //json verilerinin alınıp gönderilmesine izin verilir.
        }
        public JsonResult YorumBegen( int yorumid)
        {
            var kullanıcıId = Session[KULLANICI_ID];
            if (Session[KULLANICI_ID] == null || (int)Session[KULLANICI_ID] < 1)
            {
                return Json("Üzgünüz. Bir hata oluştur. Lütfen tekrar deneyiniz.", JsonRequestBehavior.AllowGet);
            }
            var user = db.Kullanici.Find(Session[KULLANICI_ID]);


            var yorumToUpdate = db.Yorum.Where(y => y.YorumId == yorumid).FirstOrDefault();
            yorumToUpdate.Begeni++;
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult BlogBegen(int blogid)
        {
            var kullanıcıId = Session[KULLANICI_ID];
            if (Session[KULLANICI_ID] == null || (int)Session[KULLANICI_ID] < 1)
            {
                return Json("Üzgünüz. Bir hata oluştur. Lütfen tekrar deneyiniz.", JsonRequestBehavior.AllowGet);
            }
            var user = db.Kullanici.Find(Session[KULLANICI_ID]);


            var blogToUpdate = db.Blog.Where(y => y.BlogId == blogid).FirstOrDefault();

            blogToUpdate.Begeni++;
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogKategoriPartical()
        {

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }

        public ActionResult BlogKayitPartical()
        {

            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogId).Take(5));
        }

        public ActionResult OkunmaArttir(int BlogId)
        {
            var blog = db.Blog.Where(x => x.BlogId == BlogId).SingleOrDefault();
            blog.GoruntulenmeSayisi += 1;
            db.SaveChanges();
            return View();
        }
        public ActionResult PopulerBlog() //çalışmıyor
        {

            return View(db.Blog.OrderByDescending(x => x.GoruntulenmeSayisi).Take(5));
        }

        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.Blog.Where(b => b.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(b => b.EklenmeTarihi));
        }
        public ActionResult FooterPartial()
        {

            ViewBag.OneCikanlar = db.OneCikanlar.ToList().OrderByDescending(x => x.OneCikanId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            return PartialView();
        }

        public ActionResult Begen(int id)
        {
            Blog b = db.Blog.FirstOrDefault(x => x.BlogId == id);
            b.Begeni++;
            db.SaveChanges();
            return View();

        }

        public ActionResult Kullanici(int id) //kullanıcı detay bilgileri
        {
            Kullanici kullanici = db.Kullanici.Where(x => x.KullaniciId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["kullaniciId"]) != kullanici.KullaniciId)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }
    }

}
