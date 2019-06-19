using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Models.Model
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public ICollection<KullaniciRol >KullaniciRol { get; set; }
        

    }
}