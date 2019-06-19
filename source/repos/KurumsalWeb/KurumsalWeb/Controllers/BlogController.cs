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
    public class BlogController : Controller
    {
        private KurumsalDBContext db=new KurumsalDBContext();
        // GET: Blog
        
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.BlogId));
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog,HttpPostedFileBase ResimURL) //blog modeli ve resim gelmesi lazım 
        {
            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);
                blog.ResimURL = "/Uploads/Blog/" + blogimgname;

            }

            db.Blog.Add(blog);
            db.SaveChanges();//veri tabanına yapılan değişiklikleri kaydediyoruz.
            return RedirectToAction("Index");

            
        }

        public ActionResult Edit(int id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }

            var blog = db.Blog.Where(x => x.BlogId == id).FirstOrDefault();
            if (blog==null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId=new SelectList(db.Kategori,"KategoriId","KategoriAd",blog.KategoriId);
            return View(blog);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Blog blog,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogId == id).FirstOrDefault();
                if (ResimURL != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL))
                    ) //dosya var mı yok mu onun kontorlünü sağlıyoruz.
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL));
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Blog/" + blogimgname);
                    b.ResimURL = "/Uploads/Blog/" + blogimgname;

                }

                b.Baslik = blog.Baslik;
                b.Icerik = blog.Icerik;
                b.KategoriId = blog.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(blog);
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var b=db.Blog.Find(id);
            if (b==null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(Server.MapPath(b.ResimURL))
            ) //dosya var mı yok mu onun kontorlünü sağlıyoruz.
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));//dosya varsa sil
            }

            db.Blog.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}