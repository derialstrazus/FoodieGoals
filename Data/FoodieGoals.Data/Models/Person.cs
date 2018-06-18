using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class Person
    { 
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<PersonList> PersonLists {get; set;}
        public virtual ICollection<PersonRestaurant> PersonRestaurants { get; set; }
        public virtual ICollection<PersonPhoto> ProfilePhotos { get; set; }
        public virtual ICollection<PersonFriend> Friends { get; set; }
        public virtual ICollection<PersonActivity> PersonActivities { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}