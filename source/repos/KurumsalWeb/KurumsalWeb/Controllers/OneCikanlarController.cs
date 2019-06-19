using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class OneCikanlarController : Controller
    {
       private KurumsalDBContext db=new KurumsalDBContext();//veritabanına erişim sağlıyoruz.
        // GET: OneCikanlar
        public ActionResult Index()
        {
            return View(db.OneCikanlar.ToList());
        }
        //GET
        public ActionResult Create()
        {

            return View();
        }//veri tabanına yeni bi kayıt ekleme işlemi
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OneCikanlar oneCikanlar,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null) //resim yükleme işlemi
                {
                  
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/OneCikanlar/" + logoname);
                    oneCikanlar.ResimURL = "/Uploads/OneCikanlar/" + logoname;

                }
                db.OneCikanlar.Add(oneCikanlar); //tabloya gelen öneçıkanlar modelini ekle.
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oneCikanlar);
        }

        public ActionResult Edit(int? id) //boş olabilir
        {
            if (id==null)
            {
                ViewBag.Uyari = "Güncellenecek favori bir şey bulunamadı!";
            }

            var oneCikan = db.OneCikanlar.Find(id);
            if (oneCikan==null)
            {
                return HttpNotFound();
            }
            return View(oneCikan);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(int? id, OneCikanlar oneCikanlar, HttpPostedFileBase ResimURL)
        {
            
            if (ModelState.IsValid)
            {
                var onecikan = db.OneCikanlar.Where(x => x.OneCikanId == id).FirstOrDefault();
                if (ResimURL!=null)
                {
                    
                        if (System.IO.File.Exists(Server.MapPath(onecikan.ResimURL))) //dosya var mı yok mu onun kontorlünü sağlıyoruz.
                        {
                            System.IO.File.Delete(Server.MapPath(onecikan.ResimURL));
                        }
                        WebImage img = new WebImage(ResimURL.InputStream);
                        FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                        string oneCikanName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(300, 200);
                        img.Save("~/Uploads/OneCikanlar/" + oneCikanName);
                        onecikan.ResimURL = "/Uploads/OneCikanlar/" + oneCikanName;
                   
                }

                onecikan.Baslik = oneCikanlar.Baslik;
                onecikan.Aciklama = oneCikanlar.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }

            var oneCikan = db.OneCikanlar.Find(id);//parametreden gelen id'yi tablodan bulmasını istedik.
            if (oneCikan==null)
            {
                return HttpNotFound();
            }
            else
            {
                db.OneCikanlar.Remove(oneCikan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
