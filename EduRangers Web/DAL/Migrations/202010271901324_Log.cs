namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Log : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Login");
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "Login", c => c.String());
        }
    }
}
