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
using FoodieGoals.Data.DTOs;

namespace FoodieGoals.Controllers
{
    public class RestaurantController : ApiController
    {

        private FoodieContext db = new FoodieContext();
        private DTOFactory _dtoFactory = new DTOFactory();

        public IHttpActionResult Get(int id)
        {
            Restaurant restaurant = db.Restaurants.Include(x => x.Address).FirstOrDefault(x => x.ID == id);

            return Ok(restaurant);
        }

        [HttpGet, Route("api/restaurant/search")]
        public IHttpActionResult Search([FromUri]RestaurantSearchQuery query)
        {
            List<Restaurant> restaurants;

            if (query == null)
                restaurants = db.Restaurants.Include(x => x.Tags).Include(x => x.Address).Take(20).ToList();     //This should get all your nearby restaurants
            else
            {
                restaurants = db.Restaurants
                    .Include(x => x.Tags)
                    .Include(x => x.Address)
                    .Where(x => query.SearchTerm == null || x.Name.Contains(query.SearchTerm) || x.Tags.Any(t => t.Tag.Contains(query.SearchTerm)))
                    .Take(20)
                    .ToList();
            }

            //TODO: Need to identify which ones have already been added to your goals
            
            return Ok(restaurants);
        }

        public IHttpActionResult Create(Restaurant inputRestaurant)
        {
            db.Restaurants.Add(inputRestaurant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inputRestaurant.ID }, inputRestaurant);
        }

        /*
        //UNTESTED
        public IHttpActionResult Update(Restaurant inputRestaurant)
        {
            Restaurant existingRestaurant = db.Restaurants.FirstOrDefault(x => x.ID == inputRestaurant.ID);
            //I can't copy the entire object.  That will overwrite all the attached items.
            existingRestaurant.Name = inputRestaurant.Name;     //copying property by property.  
            db.SaveChanges();

            return Ok(existingRestaurant);
        }
        */
    }

    public class RestaurantSearchQuery
    {
        public string SearchTerm { get; set; }

    }
}
