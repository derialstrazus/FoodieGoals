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
    /// 0. Enable-Migrations (Migrations folder does not exist)
    /// 1. Add-Migration MigrationName
    /// 2. Update-Database
    /// </summary>
    public class FoodieContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PersonList> PersonLists { get; set; }
        public DbSet<ListRestaurant> ListRestaurants { get; set; }
        public DbSet<PersonRestaurant> PersonRestaurants { get; set; }
        public DbSet<PersonRestaurantPhoto> PersonRestaurantPhotos { get; set; }
        public DbSet<PersonPhoto> PersonPhotos { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
