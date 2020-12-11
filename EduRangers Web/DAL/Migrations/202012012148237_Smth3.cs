namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smth3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attempts", "DateApplied", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attempts", "DateApplied");
        }
    }
}
