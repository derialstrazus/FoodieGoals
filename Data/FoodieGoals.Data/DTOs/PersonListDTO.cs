using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    public class PersonListDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Comments { get; set; }
        public IEnumerable<ListRestaurantDTO> ListRestaurants { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
