using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Yazar")]
    public class Yazar
    {
        [Key]
        public int YazarId { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır")]
        public string Adi { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır")]
        public string Soyadi { get; set; }

        public string Eposta { get; set; }
        public string Aciklama { get; set; }
        public string Sifre { get; set; }
        public string Yetki { get; set; }
        public bool? Onay { get; set; }
        public bool Aktif { get; set; } = true;
        public ICollection<Blog> Blog { get; set; }
       
      

    }
}
