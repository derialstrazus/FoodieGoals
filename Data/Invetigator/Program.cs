using FoodieGoals.Data;
using FoodieGoals.Data.DTOs;
using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invetigator
{
    class Program
    {
        static void Main(string[] args)
        {
            DTOFactory _dtoFactory = new DTOFactory();
            using (var db = new FoodieContext())
            {
                //Console.WriteLine("Enter a name for a new Restaurant: ");
                //var inputString = Console.ReadLine();

                //var restaurant = new Restaurant() {
                //    Name = inputString,
                //    Address = new Address()
                //    {
                //        StreetLine1 = "3200 N Lake Shore Dr",
                //        StreetLine2 = "Apt 2010",
                //        City = "Chicago",
                //        State = "IL",
                //        Country = "USA",
                //        PostalCode = "60657",
                //        Phone = "1234567890"
                //    }
                //};

                //db.Restaurants.Add(restaurant);
                //db.SaveChanges();

                //var query = db.Restaurants.Where(x => true);
                //Console.WriteLine("Here are all the restaurants in the database: ");
                //foreach (var item in query)
                //{
                //    Console.WriteLine(item.Name);
                //}

                //var query = db.Persons.Find(1);

                //var personRestaurant = db.PersonRestaurants.Find(1);
                //var restaurant = db.Restaurants.Find(5);

                //personRestaurant.Restaurant = restaurant;
                //db.SaveChanges();
                //Console.WriteLine(_dtoFactory.Create(personRestaurant));


                var person = db.Persons.Find(1);






                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
