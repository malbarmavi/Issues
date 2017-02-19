namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 150),
                        LastName = c.String(nullable: false, maxLength: 150),
                        Address = c.String(nullable: false, maxLength: 250),
                        Gender = c.Boolean(nullable: false),
                        Job = c.String(nullable: false, maxLength: 150),
                        DateOfCreate = c.DateTime(nullable: false),
                        DateOfUpdate = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropTable("dbo.Profiles");
        }
    }
}
