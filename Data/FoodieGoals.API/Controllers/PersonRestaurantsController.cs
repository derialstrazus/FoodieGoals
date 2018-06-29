using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FoodieGoals.Data;
using FoodieGoals.Data.DTOs;
using FoodieGoals.Data.Models;

namespace FoodieGoals.Controllers
{
    public class PersonRestaurantsController : ApiController
    {
        private FoodieContext db = new FoodieContext();
        private DTOFactory _dtoFactory = new DTOFactory();

        [HttpGet, Route("api/person/{personid}/personrestaurant/goals")]
        public IHttpActionResult GetGoals(int personid)
        {
            List<PersonRestaurant> goals = db.PersonRestaurants
                .Include(x => x.Restaurant.Address)
                .Where(x => x.Person.ID == personid && !x.HasVisited)
                .ToList();

            PersonListDTO goalList = new PersonListDTO()
            {
                ID = 0,
                Title = "Goals",
                ListRestaurants = goals.Select(x => _dtoFactory.Create(x)),
            };

            return Ok(goalList);
        }

        [HttpGet, Route("api/person/{personid}/personrestaurant/visited")]
        public IHttpActionResult GetVisited(int personid)
        {
            List<PersonRestaurant> visited = db.PersonRestaurants
                .Include(x => x.Restaurant.Address)
                .Where(x => x.Person.ID == personid && x.HasVisited)
                .ToList();

            PersonListDTO visitedList = new PersonListDTO()
            {
                ID = 0,
                Title = "Visited",
                ListRestaurants = visited.Select(x => _dtoFactory.Create(x)),
            };

            return Ok(visitedList);
        }

        // GET: api/PersonRestaurants/5
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult GetPersonRestaurant(int id)
        {
            PersonRestaurant personRestaurant = db.PersonRestaurants.Include(x => x.Restaurant.Address).SingleOrDefault(x => x.ID == id);
            if (personRestaurant == null)
                return NotFound();

            return Ok(_dtoFactory.Create(personRestaurant));
        }

        // PUT: api/PersonRestaurants/5
        // Note: For simplicity, you can pass ListRestaurantDTO in, but you have to make sure to check that the object is of type PersonRestaurant.
        // IE. the IsListRestaurant property needs to be false        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonRestaurant(int id, PersonRestaurant personRestaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != personRestaurant.ID)
                return BadRequest();

            db.Entry(personRestaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonRestaurantExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PersonRestaurants
        [HttpPost, Route("api/person/{personid}/personrestaurant/{restaurantid}", Name = "PostPersonRestaurant")]
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult PostPersonRestaurant(int personid, int restaurantid, PersonRestaurant personRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = db.Persons.Find(personid);
            if (person == null)
            {
                return NotFound();
            }
            if (person.PersonRestaurants.Any(x => x.Restaurant.ID == restaurantid))
            {
                return BadRequest("You already have this restaurant recorded.");
            }

            var restaurant = db.Restaurants.Find(restaurantid);
            if (restaurant == null)
            {
                return NotFound();
            }

            if (personRestaurant == null)
            {
                personRestaurant = new PersonRestaurant();
            }

            personRestaurant.CreatedOn = DateTime.Now;
            personRestaurant.LastEdited = DateTime.Now;
            personRestaurant.LastVisited = (DateTime)SqlDateTime.MinValue;

            person.PersonRestaurants.Add(personRestaurant);
            personRestaurant.Restaurant = restaurant;

            //db.PersonRestaurants.Add(personRestaurant);

            db.SaveChanges();

            return Ok(_dtoFactory.Create(personRestaurant));
            //return CreatedAtRoute("PostPersonRestaurant", new { personid, restaurantid }, _dtoFactory.Create(personRestaurant));
        }

        //UNTESTED
        //This has to be done recursively.  ie it will need to remove all references of that restaurant from all the ListRestaurants, and then finally from PersonRestaurant
        // DELETE: api/PersonRestaurants/5
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult DeletePersonRestaurant(int id)
        {
            PersonRestaurant personRestaurant = db.PersonRestaurants.Find(id);
            if (personRestaurant == null)
            {
                return NotFound();
            }

            var listRestaurants = db.ListRestaurants.Where(x => x.PersonRestaurant.Restaurant.ID == id);
            foreach (var listRestaurant in listRestaurants)
            {
                db.ListRestaurants.Remove(listRestaurant);
            }

            db.PersonRestaurants.Remove(personRestaurant);
            db.SaveChanges();

            return Ok(personRestaurant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonRestaurantExists(int id)
        {
            return db.PersonRestaurants.Count(e => e.ID == id) > 0;
        }
    }
}