using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("KullaniciRol")]
    public class KullaniciRol
    {
      
        public int KullaniciRolId { get; set; }
        [Key]
        public int RolId { get; set; }
     
        //public int? KullaniciId { get; set; }
        public ICollection<Rol> Rol { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}