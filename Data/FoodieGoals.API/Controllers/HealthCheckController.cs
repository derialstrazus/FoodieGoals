using FoodieGoals.Data.Managers;
using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodieGoals.Controllers
{
    [AllowAnonymous]
    public class HealthCheckController : BasicController
    {

        //http://api.foodiegoals.local/api/healthcheck
        public IHttpActionResult Get()
        {
            return Ok("Hello");
        }

        //http://api.foodiegoals.local/api/healthcheck/customroute
        [HttpGet, Route("api/healthcheck/customroute")]
        public IHttpActionResult CustomRoute()
        {
            return Ok("How did you find me?");
        }

        //http://api.foodiegoals.local/api/healthcheck/callback?value=5
        [HttpGet, Route("api/healthcheck/callback")]
        public IHttpActionResult GetCallback(int value)
        {
            return Ok(value);
        }

        //http://api.foodiegoals.local/api/healthcheck/testdatalayer?val1=2&val2=3
        [HttpGet, Route("api/healthcheck/testdatalayer")]
        public IHttpActionResult TestDataLayer(int val1, int val2)
        {
            var returnThis = HealthCheckManager.AddThese(val1, val2);
            return Ok(returnThis);
        }



        [HttpPost, Route("api/healthcheck/restaurant")]
        public IHttpActionResult TestDefault(Restaurant restaurant)
        {
            return Ok(restaurant);
        }
    }
}
