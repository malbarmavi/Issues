namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectModel : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Tasks", "Project_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "Project_Id");
            AddForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.Tasks", new[] { "Project_Id" });
            DropColumn("dbo.Tasks", "Project_Id");
            DropTable("dbo.Projects");
        }
    }
}
