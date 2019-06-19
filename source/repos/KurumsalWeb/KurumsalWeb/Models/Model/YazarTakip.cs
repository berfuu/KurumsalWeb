using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("YazarTakip")]
    public class YazarTakip
    {
        [Key]
        public int YazarId { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
        public Yazar Yazar { get; set; }

    }
}