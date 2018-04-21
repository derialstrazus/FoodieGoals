using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data
{
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
    }
}
