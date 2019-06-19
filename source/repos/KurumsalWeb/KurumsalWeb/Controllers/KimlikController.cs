
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDBContext db=new KurumsalDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

   

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid) //model bilgileri doğrulandıysa
            {
                var kimlik_bilgisi = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
                if (LogoURL!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(kimlik_bilgisi.LogoURL))) //dosya var mı yok mu onun kontorlünü sağlıyoruz.
                    {
                        System.IO.File.Delete(Server.MapPath(kimlik_bilgisi.LogoURL));
                    }
                    WebImage img=new WebImage(LogoURL.InputStream);
                    FileInfo imgInfo=new FileInfo(LogoURL.FileName);

                    string logoname = LogoURL.FileName.ToString()+imgInfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/"+logoname);
                    kimlik_bilgisi.LogoURL = "/Uploads/Kimlik/" + logoname;

                }
                kimlik_bilgisi.Title = kimlik.Title;
                kimlik_bilgisi.Keywords = kimlik.Keywords;
                kimlik_bilgisi.Description = kimlik.Description;
                kimlik_bilgisi.Unvan = kimlik.Unvan;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kimlik);
        }


     
    }
}
