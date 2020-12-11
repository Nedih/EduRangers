namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "AvgMark", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "AvgMark");
        }
    }
}
