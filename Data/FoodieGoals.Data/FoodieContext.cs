using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data
{
    /// <summary>
    /// To update database schema, use code-first migrations.
    /// 0a. Launch Package Manager Console.  Navigate "Default project" to "FoodieGoals.Data"
    /// 0b. Ensure VS can connect to database.
    /// 0c. Enable-Migrations (If running first time and migrations folder does not exist)
    /// 1. Add-Migration MigrationName
    /// 2. Update-Database
    /// </summary>
    public class FoodieContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonPhoto> PersonPhotos { get; set; }
        public DbSet<PersonRestaurant> PersonRestaurants { get; set; }
        public DbSet<PersonRestaurantPhoto> PersonRestaurantPhotos { get; set; }
        public DbSet<PersonRestaurantHistory> PersonRestaurantHistories { get; set; }
        public DbSet<PersonList> PersonLists { get; set; }
        public DbSet<ListRestaurant> ListRestaurants { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantManager> RestaurantManagers { get; set; }
        public DbSet<RestaurantTags> RestaurantTags { get; set; }
        public DbSet<RestaurantHours> RestaurantHours { get; set; }

        public DbSet<PersonFriend> Friends { get; set; }
        public DbSet<PersonActivity> Activities { get; set; }
        public DbSet<PersonActivityComment> ActivityComments { get; set; }
    }
}
