using FoodieGoals.Data;
using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodieGoals.Controllers
{
    public class RestaurantController : ApiController
    {

        private FoodieContext db = new FoodieContext();

        //[Route("api/restaurant")]
        public IHttpActionResult Get()
        {
            List<Restaurant> restaurants = db.Restaurants.Where(x => true).Take(100).ToList();

            return Ok(restaurants);
        }

        public IHttpActionResult Get(int id)
        {
            Restaurant restaurant = db.Restaurants.Include(x => x.Address).FirstOrDefault(x => x.ID == id);

            return Ok(restaurant);
        }

        //UNTESTED
        public IHttpActionResult Create(Restaurant inputRestaurant)
        {
            db.Restaurants.Add(inputRestaurant);
            db.SaveChanges();

            var savedID = inputRestaurant.ID;

            Restaurant savedRestaurant = db.Restaurants.FirstOrDefault(x => x.ID == savedID);

            if (savedRestaurant == null)
            {
                return InternalServerError(new Exception("Restaurant was not created."));
            }

            return Ok(savedRestaurant);
        }

        //UNTESTED
        public IHttpActionResult Update(Restaurant inputRestaurant)
        {
            Restaurant existingRestaurant = db.Restaurants.FirstOrDefault(x => x.ID == inputRestaurant.ID);
            //I can't copy the entire object.  That will overwrite all the attached items.
            existingRestaurant.Name = inputRestaurant.Name;     //copying property by property.  
            db.SaveChanges();

            return Ok(existingRestaurant);
        }
    }
}
