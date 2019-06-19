using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Models.DataContext
{
    public class KurumsalDBContext:DbContext
    {
        public KurumsalDBContext():base("KurumsalWebDB")
        {
            
        }

        public DbSet<Admin> Admin{ get; set; } //admin tablomuzu veritabanına set ediyoruz.
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<OneCikanlar> OneCikanlar { get; set; }
        public DbSet<Iletisim> Iletisim{ get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Kimlik> Kimlik{ get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Yazar> Yazar { get; set; }
        public DbSet<Like> Like { get; set; }



        public DbSet<Rol> Rol { get; set; }
        public DbSet<KullaniciRol> KullaniciRol { get; set; }
        public  DbSet<YazarTakip> YazarTakip { get; set; }
   
        public DbSet<Etiket> Etiket { get; set; }
        public DbSet<BlogEtiket> BlogEtiket { get; set; }
        public DbSet<YazarTakipKullanicis> YazarTakipKullanicis { get; set; }
      

    }
}

