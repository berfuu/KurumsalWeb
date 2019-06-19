namespace KurumsalWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class like2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Yorum", name: "Kullanıcı_KullaniciId", newName: "KullaniciId");
            RenameIndex(table: "dbo.Yorum", name: "IX_Kullanıcı_KullaniciId", newName: "IX_KullaniciId");
            DropColumn("dbo.Yorum", "KullanıcıId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Yorum", "KullanıcıId", c => c.Int());
            RenameIndex(table: "dbo.Yorum", name: "IX_KullaniciId", newName: "IX_Kullanıcı_KullaniciId");
            RenameColumn(table: "dbo.Yorum", name: "KullaniciId", newName: "Kullanıcı_KullaniciId");
        }
    }
}
