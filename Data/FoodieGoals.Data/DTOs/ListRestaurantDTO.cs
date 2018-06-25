using FoodieGoals.Data.Models;
using System;

namespace FoodieGoals.Data.DTOs
{
    /// <summary>
    /// Used as a DTO for both ListRestaurant and PersonRestaurant
    /// Note: when using this class as a way to pass an object in to a controller, typically via a PUT, 
    /// you have to make sure to choose the right Sequence.  The controller will accept the original model,
    /// and you have to update the DTO object to have another property called Sequence, and pass it the right one.
    /// For example, the PUT call to mark a restaurant as visited looks like this:
    /// 
    ///     var sendThis = personRestaurant;
    ///     sendThis["Sequence"] = personRestaurant.PersonRestaurantSequence;
    ///     sendThis["HasVisited"] = true;
    ///     sendThis["LastVisited"] = new Date().toISOString();
    ///     System.WebApi.Put("personrestaurants/" + personRestaurant.ID, sendThis, MarkRestaurantAsVisitedSuccess, null, personRestaurant);
    ///     
    /// </summary>
    public class ListRestaurantDTO
    {
        public int ID { get; set; }
        public string ListComment { get; set; } //only present for lists
        public int ListSequence { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }        

        //Flattened properties, from joined tables
        //From PersonRestaurant        
        public bool HasVisited { get; set; }
        public int Priority { get; set; }
        public int PersonRestaurantSequence { get; set; }
        public int Rating { get; set; }
        public DateTime? LastVisited { get; set; }
        public string Review { get; set; }
        public bool ReviewIsVisible { get; set; }
        public string Notes { get; set; }

        //From Restaurant
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Summary { get; set; }
    }
}