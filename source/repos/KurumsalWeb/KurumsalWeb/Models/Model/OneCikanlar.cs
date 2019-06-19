using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("OneCikanlar")]
    public class OneCikanlar
    {
        [Key]
        public int OneCikanId { get; set; }
        [Required,StringLength(150,ErrorMessage = "150 Karakter olabilir")]
        [DisplayName("Öne Çıkanlar Başlık")]
        public string Baslik{ get; set; }
        [DisplayName("Öne Çıkanlar Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Öne Çıkanlar Resim")]
        public string ResimURL { get; set; }
    }
}

