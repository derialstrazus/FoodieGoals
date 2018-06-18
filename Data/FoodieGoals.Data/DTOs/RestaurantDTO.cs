using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    public class RestaurantDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Summary { get; set; }
    }
}
