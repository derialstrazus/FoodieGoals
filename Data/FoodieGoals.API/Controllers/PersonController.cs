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
            Person person = db.Persons.FirstOrDefault(x => x.ID == id);

            return Ok(person);
        }
    }
}