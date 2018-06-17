namespace FoodieGoals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummaryToRestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Summary");
        }
    }
}
