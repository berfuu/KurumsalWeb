using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("BlogEtiket")]
    public class BlogEtiket
    {
        [Key]
        public int BlogId { get; set; }

        public int EtiketId  { get; set; }
     
        

    }
}