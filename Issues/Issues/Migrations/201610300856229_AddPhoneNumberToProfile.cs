namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneNumberToProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "PhoneNumber");
        }
    }
}
