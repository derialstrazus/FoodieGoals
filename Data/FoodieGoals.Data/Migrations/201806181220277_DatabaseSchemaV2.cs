namespace FoodieGoals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseSchemaV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonActivities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Activity = c.String(),
                        Person_ID = c.Int(),
                        Restaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Person_ID)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_ID)
                .Index(t => t.Person_ID)
                .Index(t => t.Restaurant_ID);
            
            CreateTable(
                "dbo.PersonActivityComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Activity_ID = c.Int(),
                        Commenter_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonActivities", t => t.Activity_ID)
                .ForeignKey("dbo.People", t => t.Commenter_ID)
                .Index(t => t.Activity_ID)
                .Index(t => t.Commenter_ID);
            
            CreateTable(
                "dbo.PersonFriends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Friend_ID = c.Int(),
                        Person_ID = c.Int(),
                        Person_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Friend_ID)
                .ForeignKey("dbo.People", t => t.Person_ID)
                .ForeignKey("dbo.People", t => t.Person_ID1)
                .Index(t => t.Friend_ID)
                .Index(t => t.Person_ID)
                .Index(t => t.Person_ID1);
            
            CreateTable(
                "dbo.PersonRestaurantHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VisitDate = c.DateTime(nullable: false),
                        PersonRestaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonRestaurants", t => t.PersonRestaurant_ID)
                .Index(t => t.PersonRestaurant_ID);
            
            CreateTable(
                "dbo.RestaurantHours",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayWeek = c.Int(nullable: false),
                        Schedule = c.String(),
                        Restaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_ID)
                .Index(t => t.Restaurant_ID);
            
            CreateTable(
                "dbo.RestaurantManagers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Manager_ID = c.Int(),
                        Restaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Manager_ID)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_ID)
                .Index(t => t.Manager_ID)
                .Index(t => t.Restaurant_ID);
            
            CreateTable(
                "dbo.RestaurantTags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TagCategory = c.String(),
                        Tag = c.String(),
                        Restaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_ID)
                .Index(t => t.Restaurant_ID);
            
            AddColumn("dbo.PersonRestaurants", "Sequence", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonActivities", "Restaurant_ID", "dbo.Restaurants");
            DropForeignKey("dbo.PersonActivityComments", "Commenter_ID", "dbo.People");
            DropForeignKey("dbo.RestaurantTags", "Restaurant_ID", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantManagers", "Restaurant_ID", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantManagers", "Manager_ID", "dbo.People");
            DropForeignKey("dbo.RestaurantHours", "Restaurant_ID", "dbo.Restaurants");
            DropForeignKey("dbo.PersonRestaurantHistories", "PersonRestaurant_ID", "dbo.PersonRestaurants");
            DropForeignKey("dbo.PersonActivities", "Person_ID", "dbo.People");
            DropForeignKey("dbo.PersonFriends", "Person_ID1", "dbo.People");
            DropForeignKey("dbo.PersonFriends", "Person_ID", "dbo.People");
            DropForeignKey("dbo.PersonFriends", "Friend_ID", "dbo.People");
            DropForeignKey("dbo.PersonActivityComments", "Activity_ID", "dbo.PersonActivities");
            DropIndex("dbo.RestaurantTags", new[] { "Restaurant_ID" });
            DropIndex("dbo.RestaurantManagers", new[] { "Restaurant_ID" });
            DropIndex("dbo.RestaurantManagers", new[] { "Manager_ID" });
            DropIndex("dbo.RestaurantHours", new[] { "Restaurant_ID" });
            DropIndex("dbo.PersonRestaurantHistories", new[] { "PersonRestaurant_ID" });
            DropIndex("dbo.PersonFriends", new[] { "Person_ID1" });
            DropIndex("dbo.PersonFriends", new[] { "Person_ID" });
            DropIndex("dbo.PersonFriends", new[] { "Friend_ID" });
            DropIndex("dbo.PersonActivityComments", new[] { "Commenter_ID" });
            DropIndex("dbo.PersonActivityComments", new[] { "Activity_ID" });
            DropIndex("dbo.PersonActivities", new[] { "Restaurant_ID" });
            DropIndex("dbo.PersonActivities", new[] { "Person_ID" });
            DropColumn("dbo.PersonRestaurants", "Sequence");
            DropTable("dbo.RestaurantTags");
            DropTable("dbo.RestaurantManagers");
            DropTable("dbo.RestaurantHours");
            DropTable("dbo.PersonRestaurantHistories");
            DropTable("dbo.PersonFriends");
            DropTable("dbo.PersonActivityComments");
            DropTable("dbo.PersonActivities");
        }
    }
}
