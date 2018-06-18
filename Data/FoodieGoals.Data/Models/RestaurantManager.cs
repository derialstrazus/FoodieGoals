using System;
using System.Collections.Generic;

namespace FoodieGoals.Data.Models
{
    public class RestaurantManager
    {
        public int ID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Person Manager { get; set; }
        public string Role { get; set; }
    }
}