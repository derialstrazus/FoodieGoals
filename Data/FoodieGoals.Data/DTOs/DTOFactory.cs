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
                LastEdited = personList.LastEdited,
                ListRestaurants = personList.ListRestaurants.Select(r => Create(r)).ToList(),
            };
        }

        public ListRestaurantDTO Create(ListRestaurant listRestaurant)
        {
            return new ListRestaurantDTO()
            {
                ID = listRestaurant.ID,
                Comment = listRestaurant.Comment,
                Sequence = listRestaurant.Sequence,
                CreatedOn = listRestaurant.CreatedOn,
                LastEdited = listRestaurant.LastEdited,
                //PersonRestaurant = Create(listRestaurant.PersonRestaurant)
                Name = listRestaurant?.PersonRestaurant?.Restaurant?.Name,
                Address = listRestaurant?.PersonRestaurant?.Restaurant?.Address,
                HasVisited = listRestaurant?.PersonRestaurant?.HasVisited != null ? listRestaurant.PersonRestaurant.HasVisited : false,
                Priority = listRestaurant?.PersonRestaurant?.Priority != null ? listRestaurant.PersonRestaurant.Priority : 0,
                Rating = listRestaurant?.PersonRestaurant?.Rating != null ? listRestaurant.PersonRestaurant.Rating : 0,
                LastVisited = listRestaurant?.PersonRestaurant?.LastVisited,
                Review = listRestaurant?.PersonRestaurant?.Review,
                ReviewIsVisible = listRestaurant?.PersonRestaurant?.ReviewIsVisible != null ? listRestaurant.PersonRestaurant.ReviewIsVisible : false,
                Notes = listRestaurant?.PersonRestaurant?.Notes
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
