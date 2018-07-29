namespace FoodieGoals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityGUIDToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IdentityID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "IdentityID");
        }
    }
}
