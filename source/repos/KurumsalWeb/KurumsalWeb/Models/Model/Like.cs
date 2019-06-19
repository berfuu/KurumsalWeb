using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required, StringLength(50, ErrorMessage = "50 Karakter olabilir")]
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? KullanıcıId { get; set; }
        public virtual Kullanici Kullanıcı { get; set; }
    }
}