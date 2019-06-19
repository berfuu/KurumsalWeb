using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;



namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Baslik { get; set; }
        public string Icerik{ get; set; }
        public string ResimURL{ get; set; }
        public int? KategoriId { get; set; }
        public int Begeni { get; set; }
        public int GoruntulenmeSayisi{ get; set; }
        public int? YazarId { get; set; }
        public System.DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public Kategori Kategori { get; set; }
        public  ICollection<Yorum> Yorums { get; set; }
        public Yazar Yazar { get; set; }
        public BlogEtiket BlogEtiket { get; set; }
        
       




    }
}

