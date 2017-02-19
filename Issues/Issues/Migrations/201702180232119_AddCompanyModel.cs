namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        DateOfCreate = c.DateTime(nullable: false),
                        DateOfUpdate = c.DateTime(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CompanyId");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            DropColumn("dbo.AspNetUsers", "CompanyId");
            DropTable("dbo.Companies");
        }
    }
}
