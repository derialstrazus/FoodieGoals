using FoodieGoals.Data.Models;
using System;

namespace FoodieGoals.Data.DTOs
{
    public class ListRestaurantDTO
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int Sequence { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }

        //public PersonRestaurantDTO PersonRestaurant { get; set; }

        //Flattened properties, from joined tables
        public string Name { get; set; }
        public Address Address { get; set; }
        public bool HasVisited { get; set; }
        public int Priority { get; set; }
        public int Rating { get; set; }
        public DateTime? LastVisited { get; set; }
        public string Review { get; set; }
        public bool ReviewIsVisible { get; set; }
        public string Notes { get; set; }
    }
}