namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteProfiles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientProfiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientProfiles", new[] { "Id" });
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropTable("dbo.ClientProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClientProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AspNetUsers", "Name");
            CreateIndex("dbo.ClientProfiles", "Id");
            AddForeignKey("dbo.ClientProfiles", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
