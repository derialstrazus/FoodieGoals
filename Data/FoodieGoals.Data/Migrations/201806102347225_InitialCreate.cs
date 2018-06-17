namespace FoodieGoals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListRestaurants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Sequence = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                        PersonList_ID = c.Int(),
                        PersonRestaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonLists", t => t.PersonList_ID)
                .ForeignKey("dbo.PersonRestaurants", t => t.PersonRestaurant_ID)
                .Index(t => t.PersonList_ID)
                .Index(t => t.PersonRestaurant_ID);
            
            CreateTable(
                "dbo.PersonLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Comments = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                        CoverPhoto_ID = c.Int(),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photos", t => t.CoverPhoto_ID)
                .ForeignKey("dbo.People", t => t.Owner_ID)
                .Index(t => t.CoverPhoto_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Company = c.String(),
                        StreetLine1 = c.String(),
                        StreetLine2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PersonRestaurants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HasVisited = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        LastVisited = c.DateTime(nullable: false),
                        Review = c.String(),
                        ReviewIsVisible = c.Boolean(nullable: false),
                        Notes = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                        Person_ID = c.Int(),
                        Restaurant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Person_ID)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_ID)
                .Index(t => t.Person_ID)
                .Index(t => t.Restaurant_ID);
            
            CreateTable(
                "dbo.PersonRestaurantPhotoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonRestaurant_ID = c.Int(),
                        Photo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonRestaurants", t => t.PersonRestaurant_ID)
                .ForeignKey("dbo.Photos", t => t.Photo_ID)
                .Index(t => t.PersonRestaurant_ID)
                .Index(t => t.Photo_ID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
            CreateTable(
                "dbo.PersonPhotoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Person_ID = c.Int(),
                        Photo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Person_ID)
                .ForeignKey("dbo.Photos", t => t.Photo_ID)
                .Index(t => t.Person_ID)
                .Index(t => t.Photo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonPhotoes", "Photo_ID", "dbo.Photos");
            DropForeignKey("dbo.PersonPhotoes", "Person_ID", "dbo.People");
            DropForeignKey("dbo.PersonRestaurants", "Restaurant_ID", "dbo.Restaurants");
            DropForeignKey("dbo.Restaurants", "Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.PersonRestaurantPhotoes", "Photo_ID", "dbo.Photos");
            DropForeignKey("dbo.PersonRestaurantPhotoes", "PersonRestaurant_ID", "dbo.PersonRestaurants");
            DropForeignKey("dbo.PersonRestaurants", "Person_ID", "dbo.People");
            DropForeignKey("dbo.ListRestaurants", "PersonRestaurant_ID", "dbo.PersonRestaurants");
            DropForeignKey("dbo.PersonLists", "Owner_ID", "dbo.People");
            DropForeignKey("dbo.People", "Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.ListRestaurants", "PersonList_ID", "dbo.PersonLists");
            DropForeignKey("dbo.PersonLists", "CoverPhoto_ID", "dbo.Photos");
            DropIndex("dbo.PersonPhotoes", new[] { "Photo_ID" });
            DropIndex("dbo.PersonPhotoes", new[] { "Person_ID" });
            DropIndex("dbo.Restaurants", new[] { "Address_ID" });
            DropIndex("dbo.PersonRestaurantPhotoes", new[] { "Photo_ID" });
            DropIndex("dbo.PersonRestaurantPhotoes", new[] { "PersonRestaurant_ID" });
            DropIndex("dbo.PersonRestaurants", new[] { "Restaurant_ID" });
            DropIndex("dbo.PersonRestaurants", new[] { "Person_ID" });
            DropIndex("dbo.People", new[] { "Address_ID" });
            DropIndex("dbo.PersonLists", new[] { "Owner_ID" });
            DropIndex("dbo.PersonLists", new[] { "CoverPhoto_ID" });
            DropIndex("dbo.ListRestaurants", new[] { "PersonRestaurant_ID" });
            DropIndex("dbo.ListRestaurants", new[] { "PersonList_ID" });
            DropTable("dbo.PersonPhotoes");
            DropTable("dbo.Restaurants");
            DropTable("dbo.PersonRestaurantPhotoes");
            DropTable("dbo.PersonRestaurants");
            DropTable("dbo.Addresses");
            DropTable("dbo.People");
            DropTable("dbo.Photos");
            DropTable("dbo.PersonLists");
            DropTable("dbo.ListRestaurants");
        }
    }
}
