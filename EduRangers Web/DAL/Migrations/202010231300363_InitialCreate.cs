namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AbilityDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        CourseDescription = c.String(),
                        Student_Id = c.String(maxLength: 128),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Login = c.String(),
                        Password = c.String(),
                        UserAvatar = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        IsApplied = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ClientProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Attempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Double(nullable: false),
                        Result = c.Boolean(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                        TestDescription = c.String(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        Answers = c.String(),
                        CorrectAnswer = c.String(),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.UserSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        count = c.Int(nullable: false),
                        Ability_Id = c.Int(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Abilities", t => t.Ability_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Ability_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CourseAbilities",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Ability_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Ability_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Abilities", t => t.Ability_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Ability_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CourseAbilities", "Ability_Id", "dbo.Abilities");
            DropForeignKey("dbo.CourseAbilities", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSlots", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSlots", "Ability_Id", "dbo.Abilities");
            DropForeignKey("dbo.Attempts", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Attempts", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseAbilities", new[] { "Ability_Id" });
            DropIndex("dbo.CourseAbilities", new[] { "Course_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserSlots", new[] { "Owner_Id" });
            DropIndex("dbo.UserSlots", new[] { "Ability_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Tests", new[] { "Course_Id" });
            DropIndex("dbo.Attempts", new[] { "Test_Id" });
            DropIndex("dbo.Attempts", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ClientProfiles", new[] { "Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Courses", new[] { "Author_Id" });
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            DropTable("dbo.CourseAbilities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserSlots");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.Attempts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ClientProfiles");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Courses");
            DropTable("dbo.Abilities");
        }
    }
}
