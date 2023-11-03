namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Cognome", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Indirizzo", c => c.String());
            AddColumn("dbo.AspNetUsers", "GustoFavorito", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Telefono", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Telefono");
            DropColumn("dbo.AspNetUsers", "GustoFavorito");
            DropColumn("dbo.AspNetUsers", "Indirizzo");
            DropColumn("dbo.AspNetUsers", "Cognome");
            DropColumn("dbo.AspNetUsers", "Nome");
        }
    }
}
