namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTasksModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Statement = c.String(nullable: false, maxLength: 250),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        State = c.Int(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        DateOfUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TasksApplicationUsers",
                c => new
                    {
                        Tasks_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Tasks_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Tasks", t => t.Tasks_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Tasks_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TasksApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksApplicationUsers", "Tasks_Id", "dbo.Tasks");
            DropIndex("dbo.TasksApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TasksApplicationUsers", new[] { "Tasks_Id" });
            DropTable("dbo.TasksApplicationUsers");
            DropTable("dbo.Tasks");
        }
    }
}
