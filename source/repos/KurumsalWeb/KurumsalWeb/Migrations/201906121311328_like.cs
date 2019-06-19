namespace KurumsalWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class like : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(nullable: false, maxLength: 50),
                        Eposta = c.String(),
                        BlogId = c.Int(),
                        KullanıcıId = c.Int(),
                        Kullanıcı_KullaniciId = c.Int(),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Blog", t => t.BlogId)
                .ForeignKey("dbo.Kullanici", t => t.Kullanıcı_KullaniciId)
                .Index(t => t.BlogId)
                .Index(t => t.Kullanıcı_KullaniciId);
            
            AddColumn("dbo.Yorum", "KullanıcıId", c => c.Int());
            AddColumn("dbo.Yorum", "Kullanıcı_KullaniciId", c => c.Int());
            CreateIndex("dbo.Yorum", "Kullanıcı_KullaniciId");
            AddForeignKey("dbo.Yorum", "Kullanıcı_KullaniciId", "dbo.Kullanici", "KullaniciId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "Kullanıcı_KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Likes", "BlogId", "dbo.Blog");
            DropForeignKey("dbo.Yorum", "Kullanıcı_KullaniciId", "dbo.Kullanici");
            DropIndex("dbo.Likes", new[] { "Kullanıcı_KullaniciId" });
            DropIndex("dbo.Likes", new[] { "BlogId" });
            DropIndex("dbo.Yorum", new[] { "Kullanıcı_KullaniciId" });
            DropColumn("dbo.Yorum", "Kullanıcı_KullaniciId");
            DropColumn("dbo.Yorum", "KullanıcıId");
            DropTable("dbo.Likes");
        }
    }
}
