using FoodieGoals.Data.Models;
using System;

namespace FoodieGoals.Data.DTOs
{
    /// <summary>
    /// Used as a DTO for both ListRestaurant and PersonRestaurant
    /// </summary>
    public class ListRestaurantDTO
    {
        public int ID { get; set; }
        public string ListComment { get; set; } //only present for lists
        public int Sequence { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }        

        //Flattened properties, from joined tables
        //From PersonRestaurant        
        public bool HasVisited { get; set; }
        public int Priority { get; set; }        
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

        public bool IsListRestaurant { get; set; }      //If false, IsPersonRestaurant
    }
}