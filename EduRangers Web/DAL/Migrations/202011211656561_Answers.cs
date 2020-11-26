namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Answers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            DropColumn("dbo.Questions", "Answers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Answers", c => c.String());
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropTable("dbo.Answers");
        }
    }
}
