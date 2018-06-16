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
    public class PersonController : ApiController
    {
        private FoodieContext db = new FoodieContext();
        private DTOFactory _dtoFactory = new DTOFactory();

        public IHttpActionResult Get(int id)
        {
            try
            {
                Person person = db.Persons
                .Include(x => x.PersonLists)
                .FirstOrDefault(x => x.ID == id);

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



    }
}