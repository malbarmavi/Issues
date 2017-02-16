namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTaskModeIdTypeToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TasksApplicationUsers", "Tasks_Id", "dbo.Tasks");
            DropIndex("dbo.TasksApplicationUsers", new[] { "Tasks_Id" });
            DropPrimaryKey("dbo.Tasks");
            DropPrimaryKey("dbo.TasksApplicationUsers");
            AlterColumn("dbo.Tasks", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TasksApplicationUsers", "Tasks_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Tasks", "Id");
            AddPrimaryKey("dbo.TasksApplicationUsers", new[] { "Tasks_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.TasksApplicationUsers", "Tasks_Id");
            AddForeignKey("dbo.TasksApplicationUsers", "Tasks_Id", "dbo.Tasks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TasksApplicationUsers", "Tasks_Id", "dbo.Tasks");
            DropIndex("dbo.TasksApplicationUsers", new[] { "Tasks_Id" });
            DropPrimaryKey("dbo.TasksApplicationUsers");
            DropPrimaryKey("dbo.Tasks");
            AlterColumn("dbo.TasksApplicationUsers", "Tasks_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Tasks", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.TasksApplicationUsers", new[] { "Tasks_Id", "ApplicationUser_Id" });
            AddPrimaryKey("dbo.Tasks", "Id");
            CreateIndex("dbo.TasksApplicationUsers", "Tasks_Id");
            AddForeignKey("dbo.TasksApplicationUsers", "Tasks_Id", "dbo.Tasks", "Id", cascadeDelete: true);
        }
    }
}
