using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider Başlık"),StringLength(30)]
        public string Baslik { get; set; }
        [DisplayName("Slider Açıklama"),StringLength(150)]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"),StringLength(250)]
        public string ResimURL { get; set; }
    }
}