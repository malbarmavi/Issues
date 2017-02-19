namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TasksApplicationUsers", newName: "ApplicationUserTasks");
            DropPrimaryKey("dbo.ApplicationUserTasks");
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 250),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateOfCreate = c.DateTime(nullable: false),
                        DateOfUpdate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            AddColumn("dbo.Tasks", "ProjectId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ApplicationUserTasks", new[] { "ApplicationUser_Id", "Tasks_Id" });
            CreateIndex("dbo.Tasks", "ProjectId");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropPrimaryKey("dbo.ApplicationUserTasks");
            DropColumn("dbo.Tasks", "ProjectId");
            DropTable("dbo.Projects");
            AddPrimaryKey("dbo.ApplicationUserTasks", new[] { "Tasks_Id", "ApplicationUser_Id" });
            RenameTable(name: "dbo.ApplicationUserTasks", newName: "TasksApplicationUsers");
        }
    }
}
