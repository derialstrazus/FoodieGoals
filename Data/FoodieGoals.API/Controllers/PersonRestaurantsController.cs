using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // GET: api/PersonRestaurants
        public IQueryable<PersonRestaurant> GetPersonRestaurants()
        {
            return db.PersonRestaurants;
        }

        [HttpGet, Route("api/person/{personid}/personrestaurant/goals")]
        public IHttpActionResult GetGoals(int personid)
        {
            List<PersonRestaurant> goals = db.PersonRestaurants
                .Include(x => x.Restaurant)
                .Where(x => x.Person.ID == personid && !x.HasVisited)
                .ToList();
            List<PersonRestaurantDTO> personRestaurantDTOs = goals.Select(x => _dtoFactory.Create(x)).ToList();
            return Ok(personRestaurantDTOs);
        }

        // GET: api/PersonRestaurants/5
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult GetPersonRestaurant(int id)
        {
            PersonRestaurant personRestaurant = db.PersonRestaurants.Find(id);
            if (personRestaurant == null)
            {
                return NotFound();
            }

            return Ok(personRestaurant);
        }

        // PUT: api/PersonRestaurants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonRestaurant(int id, PersonRestaurant personRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personRestaurant.ID)
            {
                return BadRequest();
            }

            db.Entry(personRestaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonRestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PersonRestaurants
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult PostPersonRestaurant(PersonRestaurant personRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonRestaurants.Add(personRestaurant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personRestaurant.ID }, personRestaurant);
        }

        // DELETE: api/PersonRestaurants/5
        [ResponseType(typeof(PersonRestaurant))]
        public IHttpActionResult DeletePersonRestaurant(int id)
        {
            PersonRestaurant personRestaurant = db.PersonRestaurants.Find(id);
            if (personRestaurant == null)
            {
                return NotFound();
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