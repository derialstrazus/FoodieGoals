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
    public class PersonListController : ApiController
    {
        private FoodieContext db = new FoodieContext();
        private DTOFactory _dtoFactory = new DTOFactory();

        /// <summary>
        /// No Paging
        /// </summary>
        [HttpGet, Route("api/personlist/{id}/all"), ResponseType(typeof(PersonList))]
        public IHttpActionResult GetPersonListAll(int id)
        {
            PersonList personList = db.PersonLists.Find(id);
            if (personList == null)
            {
                return NotFound();
            }
            return Ok(_dtoFactory.Create(personList));
        }

        // GET: api/PersonList/5?page=1&pagesize=10
        [ResponseType(typeof(PersonList))]
        public IHttpActionResult GetPersonList(int id, int page = 1, int pagesize = 10)
        {
            //Get
            PersonList personList = db.PersonLists.Find(id);
            if (personList == null)
            {
                return NotFound();
            }

            //Page
            var listRestaurants = db.ListRestaurants                
                .Include(x => x.PersonRestaurant.Restaurant.Address)
                .Where(x => x.PersonList.ID == id)
                .OrderBy(x => x.Sequence).ThenBy(x => x.ID)
                .Skip(pagesize * (page - 1))
                .Take(pagesize)
                .ToList();

            //Copy
            personList.ListRestaurants.Clear();
            foreach (var item in listRestaurants)
            {
                personList.ListRestaurants.Add(item);
            }

            //Convert
            PersonListDTO returnThis = _dtoFactory.Create(personList);

            return Ok(returnThis);
        }

        //// PUT: api/PersonList/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPersonList(int id, PersonList personList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != personList.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(personList).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PersonListExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/PersonList
        //[ResponseType(typeof(PersonList))]
        //public IHttpActionResult PostPersonList(PersonList personList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.PersonLists.Add(personList);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = personList.ID }, personList);
        //}

        //// DELETE: api/PersonList/5
        //[ResponseType(typeof(PersonList))]
        //public IHttpActionResult DeletePersonList(int id)
        //{
        //    PersonList personList = db.PersonLists.Find(id);
        //    if (personList == null)
        //    {
        //        return NotFound();
        //    }

        //    db.PersonLists.Remove(personList);
        //    db.SaveChanges();

        //    return Ok(personList);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonListExists(int id)
        {
            return db.PersonLists.Count(e => e.ID == id) > 0;
        }
    }
}