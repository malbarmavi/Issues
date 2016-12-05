namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaskStatemntAndNameTextLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tasks", "Statement", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Statement", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "Name", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
