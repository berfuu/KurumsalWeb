 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Yorum")]
    public class Yorum
    {
       
        [Key]
        public int YorumId { get; set; }
        [Required,StringLength(50,ErrorMessage = "50 Karakter olabilir")]

        public string AdSoyad { get; set; }
        public string Eposta { get; set; }

        private string icerik;
        [DisplayName("Yorumunuz")]
        public string Icerik
        {
            get { return icerik; }
            set { icerik = KillChars(value); }
        }


        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime EklenmeTarihi { get; set; } 
        public int Begeni { get; set; }
        public bool Onay
        {
            get;
            set;
        }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        
        public int? KullaniciId { get; set; }
        public Kullanici Kullanıcı { get; set; }


        public bool IsEligible
        {
            get
            {
                if (KeywordCheck())
                    return true;
                return false;
            }
        }

        private bool KeywordCheck()
        {
            if (Icerik.ToLower().Contains("hello") || Icerik.ToLower().Contains("teyto"))
                return false;
            return true;
        }
        public string KillChars(string strWords)
        {
            string newChars = "";
            string[] InjectionArray = new string[43];

            InjectionArray[0] = "select";
            InjectionArray[1] = "update";
            InjectionArray[2] = "insert";
            InjectionArray[3] = "Insert";
            InjectionArray[4] = "delete";
            InjectionArray[5] = "from ";
            InjectionArray[6] = "where";
            InjectionArray[7] = "union";
            InjectionArray[8] = "alter";
            InjectionArray[9] = "dbcc";
            InjectionArray[10] = "dbo.";
            InjectionArray[11] = "exec";
            InjectionArray[12] = "<=";
            InjectionArray[13] = ">=";
            InjectionArray[14] = "having";
            InjectionArray[15] = "join";
            InjectionArray[16] = "master.";
            InjectionArray[17] = "model.";
            InjectionArray[18] = "msdb";
            InjectionArray[19] = "isnull(";
            InjectionArray[20] = "is null";
            InjectionArray[21] = "null";
            InjectionArray[22] = "'";
            InjectionArray[23] = "|";
            InjectionArray[24] = "tempdb";
            InjectionArray[25] = "truncate";
            InjectionArray[26] = "trunc";
            InjectionArray[27] = "xp_cmdshell";
            InjectionArray[28] = "xp_startmail";
            InjectionArray[29] = "xp_senKoskail";
            InjectionArray[30] = "xp_makewebtask";
            InjectionArray[31] = "shutdown";
            InjectionArray[32] = "char";
            InjectionArray[33] = "sp_";
            InjectionArray[34] = "--";
            InjectionArray[35] = "drop";
            InjectionArray[36] = ";";
            InjectionArray[37] = "--";
            InjectionArray[38] = "insert";
            InjectionArray[39] = "delete";
            InjectionArray[40] = "xp_";
            InjectionArray[41] = "<";
            InjectionArray[42] = ">";


            newChars = strWords;

            int i;
            for (i = 0; i <= InjectionArray.GetUpperBound(0); i++)
            {
                if (InjectionArray[i] == "'")
                {
                    newChars = newChars.Replace(InjectionArray[i], "''");
                }
                else
                {
                    newChars = newChars.Replace(InjectionArray[i], "");
                }
            }
            return newChars;
        }
    }
}