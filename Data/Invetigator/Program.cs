using FoodieGoals.Data;
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

                var query = db.Persons.Find(1);

                var test = new ListRestaurant()
                {
                    PersonRestaurant = new PersonRestaurant()
                    {
                        Rating = 2,
                        Restaurant = null
                    }
                };

                String truth1 = test?.PersonRestaurant?.Restaurant?.Name;
                int? truth2 = test?.PersonRestaurant?.Rating;

                Console.WriteLine(query);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
