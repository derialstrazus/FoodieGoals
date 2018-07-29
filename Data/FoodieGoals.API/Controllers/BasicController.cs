using FoodieGoals.Data;
using FoodieGoals.Data.DTOs;
using FoodieGoals.Data.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FoodieGoals.Controllers
{
    public class BasicController : ApiController
    {
        public FoodieContext db = new FoodieContext();
        public DTOFactory _dtoFactory = new DTOFactory();

        public bool IsLoggedIn
        {
            get
            {
                return RequestContext.Principal.Identity.IsAuthenticated;
            }
        }

        private Person _currentPerson;
        public Person CurrentPerson
        {
            get
            {
                var userId = User.Identity.GetUserId();
                if (_currentPerson == null || _currentPerson.IdentityID != userId)
                {
                    _currentPerson = db.Persons.Where(x => x.IdentityID == userId).FirstOrDefault();
                }

                return _currentPerson;
            }
        }        
    }
}