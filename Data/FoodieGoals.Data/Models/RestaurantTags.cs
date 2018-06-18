using System;
using System.Collections.Generic;

namespace FoodieGoals.Data.Models
{
    public class RestaurantTags
    {
        public int ID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public string TagCategory { get; set; }
        public string Tag { get; set; }
    }
}