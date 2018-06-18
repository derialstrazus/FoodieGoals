using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    /// <summary>
    /// Use this to transform entities into DTOs
    /// </summary>
    public class DTOFactory
    {

        public PersonDTO Create(Person person)
        {
            return new PersonDTO()
            {
                ID = person.ID,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Email = person.Email,
                Address = person.Address,
                CreatedOn = person.CreatedOn,
                LastEdited = person.LastEdited,
                PersonLists = person.PersonLists.Select(pl => Create(pl)).ToList()
            };
        }


        public PersonListDTO Create(PersonList personList)
        {
            return new PersonListDTO()
            {
                ID = personList.ID,
                Title = personList.Title,
                Comments = personList.Comments,
                CreatedOn = personList.CreatedOn,
                LastEdited = personList.LastEdited
            };
        }

        public PersonRestaurantDTO Create(PersonRestaurant personRestaurant)
        {
            return new PersonRestaurantDTO()
            {
                ID = personRestaurant.ID,
                HasVisited = personRestaurant.HasVisited,
                Priority = personRestaurant.Priority,
                Sequence = personRestaurant.Sequence,
                Rating = personRestaurant.Rating,
                LastVisited = personRestaurant.LastVisited,
                Review = personRestaurant.Review,
                ReviewIsVisible = personRestaurant.ReviewIsVisible,
                Notes = personRestaurant.Notes,
                CreatedOn = personRestaurant.CreatedOn,
                LastEdited = personRestaurant.LastEdited,
                Restaurant = Create(personRestaurant.Restaurant)
            };
        }

        public RestaurantDTO Create(Restaurant restaurant)
        {
            return new RestaurantDTO()
            {
                ID = restaurant.ID,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Summary = restaurant.Summary
            };
        }

    }
}
