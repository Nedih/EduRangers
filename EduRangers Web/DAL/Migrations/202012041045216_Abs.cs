namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abilities", "AbilityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Abilities", "AbilityName");
        }
    }
}
