namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BanAbilities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseAbilities", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseAbilities", "Ability_Id", "dbo.Abilities");
            DropIndex("dbo.CourseAbilities", new[] { "Course_Id" });
            DropIndex("dbo.CourseAbilities", new[] { "Ability_Id" });
            DropPrimaryKey("dbo.CourseAbilities");
            AddColumn("dbo.CourseAbilities", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CourseAbilities", "Course_Id", c => c.Int());
            AlterColumn("dbo.CourseAbilities", "Ability_Id", c => c.Int());
            AddPrimaryKey("dbo.CourseAbilities", "Id");
            CreateIndex("dbo.CourseAbilities", "Ability_Id");
            CreateIndex("dbo.CourseAbilities", "Course_Id");
            AddForeignKey("dbo.CourseAbilities", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.CourseAbilities", "Ability_Id", "dbo.Abilities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseAbilities", "Ability_Id", "dbo.Abilities");
            DropForeignKey("dbo.CourseAbilities", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseAbilities", new[] { "Course_Id" });
            DropIndex("dbo.CourseAbilities", new[] { "Ability_Id" });
            DropPrimaryKey("dbo.CourseAbilities");
            AlterColumn("dbo.CourseAbilities", "Ability_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseAbilities", "Course_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CourseAbilities", "Id");
            AddPrimaryKey("dbo.CourseAbilities", new[] { "Course_Id", "Ability_Id" });
            CreateIndex("dbo.CourseAbilities", "Ability_Id");
            CreateIndex("dbo.CourseAbilities", "Course_Id");
            AddForeignKey("dbo.CourseAbilities", "Ability_Id", "dbo.Abilities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseAbilities", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
