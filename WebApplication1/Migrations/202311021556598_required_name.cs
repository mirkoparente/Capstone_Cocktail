namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required_name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Nome", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Cognome", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "GustoFavorito", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GustoFavorito", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Cognome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Nome", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
