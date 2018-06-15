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
    public class PersonController : ApiController
    {
        private FoodieContext db = new FoodieContext();

        public IHttpActionResult Get(int id)
        {
            PersonDTO person = db.Persons
                .Include(x => x.PersonLists)
                .Select(x => new PersonDTO() {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    CreatedOn = x.CreatedOn,
                    LastEdited = x.LastEdited,
                    PersonLists = x.PersonLists.Select(y => new PersonListDTO() {
                        ID = y.ID,
                        Title = y.Title,
                        Comments = y.Comments,
                        CreatedOn = y.CreatedOn,
                        LastEdited = y.LastEdited
                    }).ToList()
                })
                .FirstOrDefault(x => x.ID == id);

            return Ok(person);
        }
    }
}