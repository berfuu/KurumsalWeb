using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;



namespace KurumsalWeb.Models.Model
{
    [Table("Kullanici")] //tablo adı
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; } //veritabanımızdaki kolonlara karşılık.
        [Required, StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        public string Adi { get; set; }

        public string Soyadi { get; set; }
        public string KullaniciAdi { get; set; }
        public string Eposta { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        public string Sifre { get; set; }
        public bool Aktif { get; set; }
        public bool Onay { get; set; }
        public bool Yazar { get; set; }
        public string Yetki { get; set; }

        public string Foto { get; set; }



        public Blog Blog { get; set; }
        public ICollection<KullaniciRol> KullaniciRol { get; set; }
        public ICollection<Yorum> Yorumlar { get; set; }





    }
}




