namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smth2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tests", "AvgMark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "AvgMark", c => c.Double(nullable: false));
        }
    }
}
