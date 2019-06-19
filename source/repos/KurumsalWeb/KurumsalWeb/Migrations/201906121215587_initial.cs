namespace KurumsalWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogEtiket",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        EtiketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId);
            
            CreateTable(
                "dbo.Yazar",
                c => new
                    {
                        YazarId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(nullable: false, maxLength: 50),
                        Eposta = c.String(),
                        Aciklama = c.String(),
                        Sifre = c.String(),
                        Yetki = c.String(),
                        Onay = c.Boolean(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.YazarId);
            
            CreateTable(
                "dbo.Etiket",
                c => new
                    {
                        EtiketId = c.Int(nullable: false, identity: true),
                        EtiketAdi = c.String(),
                        BlogEtiket_BlogId = c.Int(),
                    })
                .PrimaryKey(t => t.EtiketId)
                .ForeignKey("dbo.BlogEtiket", t => t.BlogEtiket_BlogId)
                .Index(t => t.BlogEtiket_BlogId);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        KullaniciId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(),
                        KullaniciAdi = c.String(),
                        Eposta = c.String(),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        Aktif = c.Boolean(nullable: false),
                        Onay = c.Boolean(nullable: false),
                        Yazar = c.Boolean(nullable: false),
                        Yetki = c.String(),
                        Foto = c.String(),
                        Blog_BlogId = c.Int(),
                    })
                .PrimaryKey(t => t.KullaniciId)
                .ForeignKey("dbo.Blog", t => t.Blog_BlogId)
                .Index(t => t.Blog_BlogId);
            
            CreateTable(
                "dbo.KullaniciRol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        KullaniciRolId = c.Int(nullable: false),
                        Kullanici_KullaniciId = c.Int(),
                    })
                .PrimaryKey(t => t.RolId)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_KullaniciId)
                .Index(t => t.Kullanici_KullaniciId);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.OneCikanlar",
                c => new
                    {
                        OneCikanId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(),
                        ResimURL = c.String(),
                    })
                .PrimaryKey(t => t.OneCikanId);
            
            CreateTable(
                "dbo.YazarTakip",
                c => new
                    {
                        YazarId = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        Yazar_YazarId = c.Int(),
                    })
                .PrimaryKey(t => t.YazarId)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId, cascadeDelete: true)
                .ForeignKey("dbo.Yazar", t => t.Yazar_YazarId)
                .Index(t => t.KullaniciId)
                .Index(t => t.Yazar_YazarId);
            
            CreateTable(
                "dbo.YazarTakipKullanicis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolKullaniciRols",
                c => new
                    {
                        Rol_RolId = c.Int(nullable: false),
                        KullaniciRol_RolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rol_RolId, t.KullaniciRol_RolId })
                .ForeignKey("dbo.Rol", t => t.Rol_RolId, cascadeDelete: true)
                .ForeignKey("dbo.KullaniciRol", t => t.KullaniciRol_RolId, cascadeDelete: true)
                .Index(t => t.Rol_RolId)
                .Index(t => t.KullaniciRol_RolId);
            
            AddColumn("dbo.Blog", "Begeni", c => c.Int(nullable: false));
            AddColumn("dbo.Blog", "GoruntulenmeSayisi", c => c.Int(nullable: false));
            AddColumn("dbo.Blog", "YazarId", c => c.Int());
            AddColumn("dbo.Blog", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Blog", "BlogEtiket_BlogId", c => c.Int());
            AddColumn("dbo.Yorum", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Yorum", "Begeni", c => c.Int(nullable: false));
            CreateIndex("dbo.Blog", "YazarId");
            CreateIndex("dbo.Blog", "BlogEtiket_BlogId");
            AddForeignKey("dbo.Blog", "BlogEtiket_BlogId", "dbo.BlogEtiket", "BlogId");
            AddForeignKey("dbo.Blog", "YazarId", "dbo.Yazar", "YazarId");
            DropTable("dbo.Hizmet");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Hizmet",
                c => new
                    {
                        HizmetId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(),
                        ResimURL = c.String(),
                    })
                .PrimaryKey(t => t.HizmetId);
            
            DropForeignKey("dbo.YazarTakip", "Yazar_YazarId", "dbo.Yazar");
            DropForeignKey("dbo.YazarTakip", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.RolKullaniciRols", "KullaniciRol_RolId", "dbo.KullaniciRol");
            DropForeignKey("dbo.RolKullaniciRols", "Rol_RolId", "dbo.Rol");
            DropForeignKey("dbo.KullaniciRol", "Kullanici_KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "Blog_BlogId", "dbo.Blog");
            DropForeignKey("dbo.Etiket", "BlogEtiket_BlogId", "dbo.BlogEtiket");
            DropForeignKey("dbo.Blog", "YazarId", "dbo.Yazar");
            DropForeignKey("dbo.Blog", "BlogEtiket_BlogId", "dbo.BlogEtiket");
            DropIndex("dbo.RolKullaniciRols", new[] { "KullaniciRol_RolId" });
            DropIndex("dbo.RolKullaniciRols", new[] { "Rol_RolId" });
            DropIndex("dbo.YazarTakip", new[] { "Yazar_YazarId" });
            DropIndex("dbo.YazarTakip", new[] { "KullaniciId" });
            DropIndex("dbo.KullaniciRol", new[] { "Kullanici_KullaniciId" });
            DropIndex("dbo.Kullanici", new[] { "Blog_BlogId" });
            DropIndex("dbo.Etiket", new[] { "BlogEtiket_BlogId" });
            DropIndex("dbo.Blog", new[] { "BlogEtiket_BlogId" });
            DropIndex("dbo.Blog", new[] { "YazarId" });
            DropColumn("dbo.Yorum", "Begeni");
            DropColumn("dbo.Yorum", "EklenmeTarihi");
            DropColumn("dbo.Blog", "BlogEtiket_BlogId");
            DropColumn("dbo.Blog", "EklenmeTarihi");
            DropColumn("dbo.Blog", "YazarId");
            DropColumn("dbo.Blog", "GoruntulenmeSayisi");
            DropColumn("dbo.Blog", "Begeni");
            DropTable("dbo.RolKullaniciRols");
            DropTable("dbo.YazarTakipKullanicis");
            DropTable("dbo.YazarTakip");
            DropTable("dbo.OneCikanlar");
            DropTable("dbo.Rol");
            DropTable("dbo.KullaniciRol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Etiket");
            DropTable("dbo.Yazar");
            DropTable("dbo.BlogEtiket");
        }
    }
}
