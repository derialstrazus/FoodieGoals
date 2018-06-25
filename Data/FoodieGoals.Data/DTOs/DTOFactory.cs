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

        //ListRestaurant.PersonRestaurant.Restaurant
        //Both ListRestaurant and PersonRestaurant converts to ListRestaurantDTO
        //This is because when you're getting a list of PersonRestaurant, its usually as Goals or Visited, which is treated as if they were lists themselves
        public ListRestaurantDTO Create(ListRestaurant listRestaurant)
        {            
            return new ListRestaurantDTO()
            {
                ID = listRestaurant.ID,
                ListComment = listRestaurant.Comment,
                ListSequence = listRestaurant.Sequence,
                CreatedOn = listRestaurant.CreatedOn,
                LastEdited = listRestaurant.LastEdited,
                RestaurantID = listRestaurant?.PersonRestaurant?.Restaurant?.ID != null ? listRestaurant.PersonRestaurant.Restaurant.ID: 0,
                Name = listRestaurant?.PersonRestaurant?.Restaurant?.Name,
                Address = listRestaurant?.PersonRestaurant?.Restaurant?.Address,
                Summary = listRestaurant?.PersonRestaurant?.Restaurant?.Summary,
                HasVisited = listRestaurant?.PersonRestaurant?.HasVisited != null ? listRestaurant.PersonRestaurant.HasVisited : false,
                Priority = listRestaurant?.PersonRestaurant?.Priority != null ? listRestaurant.PersonRestaurant.Priority : 0,
                PersonRestaurantSequence = 0,
                Rating = listRestaurant?.PersonRestaurant?.Rating != null ? listRestaurant.PersonRestaurant.Rating : 0,
                LastVisited = listRestaurant?.PersonRestaurant?.LastVisited,
                Review = listRestaurant?.PersonRestaurant?.Review,
                ReviewIsVisible = listRestaurant?.PersonRestaurant?.ReviewIsVisible != null ? listRestaurant.PersonRestaurant.ReviewIsVisible : false,
                Notes = listRestaurant?.PersonRestaurant?.Notes
            };
        }

        public ListRestaurantDTO Create(PersonRestaurant personRestaurant)
        {
            return new ListRestaurantDTO()
            {
                ID = personRestaurant.ID,
                ListComment = null,
                ListSequence = 0,
                CreatedOn = personRestaurant.CreatedOn,
                LastEdited = personRestaurant.LastEdited,
                HasVisited = personRestaurant.HasVisited,
                Priority = personRestaurant.Priority,
                PersonRestaurantSequence = personRestaurant.Sequence,
                Rating = personRestaurant.Rating,
                LastVisited = personRestaurant.LastVisited,
                Review = personRestaurant.Review,
                ReviewIsVisible = personRestaurant.ReviewIsVisible,
                Notes = personRestaurant.Notes,
                RestaurantID = personRestaurant?.Restaurant?.ID != null ? personRestaurant.Restaurant.ID : 0,
                Name = personRestaurant?.Restaurant?.Name,
                Address = personRestaurant?.Restaurant?.Address,
                Summary = personRestaurant?.Restaurant?.Summary,
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
