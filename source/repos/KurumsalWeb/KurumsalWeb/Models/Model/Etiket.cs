using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Etiket")]
    public class Etiket
    {
        [Key]
        public int EtiketId { get; set; }
        public string EtiketAdi { get; set; }
        public BlogEtiket BlogEtiket { get; set; }
       
    }
}