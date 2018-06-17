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
using System.Data.Entity.Infrastructure;

namespace FoodieGoals.Controllers
{
    public class PersonController : ApiController
    {
        private FoodieContext db = new FoodieContext();
        private DTOFactory _dtoFactory = new DTOFactory();

        public IHttpActionResult Get(int id)
        {
            try
            {
                //Person person = db.Persons
                //.Include(x => x.PersonLists)
                //.Include(x => x.Address)
                //.FirstOrDefault(x => x.ID == id);

                Person person = db.Persons.Find(id);

                if (person == null)
                    return NotFound();

                PersonDTO personDTO = _dtoFactory.Create(person);
                return Ok(personDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// This is a temporary API, just so that I can build something without having authentication available
        /// </summary>
        /// <returns></returns>
        [Route("api/person/users")]
        public IHttpActionResult GetPeople()
        {
            try
            {
                List<Person> people = db.Persons.Take(10).OrderBy(x => x.ID).ToList();
                List<PersonDTO> peopleDTO = people.Select(x => _dtoFactory.Create(x)).ToList();

                return Ok(peopleDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostPerson(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.ID }, person);
        }

        // PUT: api/Person/5
        public IHttpActionResult PutPerson(int id, Person person)
        {
            if (id != person.ID)            
                return BadRequest();            

            db.Entry(person).State = EntityState.Modified;            
            db.SaveChanges();

            return Ok(_dtoFactory.Create(person));
        }

        //#region Address
        [HttpPost, Route("api/person/{personid}/address")]
        public IHttpActionResult CreatePersonAddress(int personid, Address address)
        {
            var person = db.Persons.Find(personid);
            if (person == null)
                return NotFound();
            else if (person.Address != null)
                return BadRequest("person already has an existing address.  Use PUT instead.");

            person.Address = address;
            db.SaveChanges();

            person = db.Persons.Find(personid);
            return Ok(_dtoFactory.Create(person));
        }

        [HttpPut, Route("api/person/{personid}/address")]
        public IHttpActionResult UpdatePersonAddress(int personid, Address address)
        {
            var person = db.Persons.AsNoTracking().Include(x => x.Address).FirstOrDefault(x => x.ID == personid);
            if (person == null)
                return NotFound();
            else if (person.Address == null)
                return BadRequest("person does not have an existing address.  Use POST instead.");
            else if (person.Address.ID != address.ID)
                return BadRequest("address on person does not match id of address in body.");

            db.Entry(address).State = EntityState.Modified;
            db.SaveChanges();

            person = db.Persons.Find(personid);
            return Ok(_dtoFactory.Create(person));
        }
        //#endregion
    }
}