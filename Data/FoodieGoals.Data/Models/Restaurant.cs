﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Summary { get; set; }
        //public Menu Menu { get; set; }

        public virtual ICollection<RestaurantManager> Managers { get; set; }
        public virtual ICollection<RestaurantTags> Tags { get; set; }
        public virtual ICollection<RestaurantHours> Hours { get; set; }

    }
}