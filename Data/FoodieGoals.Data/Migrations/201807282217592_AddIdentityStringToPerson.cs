namespace FoodieGoals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityStringToPerson : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "IdentityID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "IdentityID", c => c.Guid(nullable: false));
        }
    }
}
