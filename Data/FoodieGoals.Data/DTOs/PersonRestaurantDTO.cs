using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    [Obsolete("Use ListRestaurantDTO instead")]
    public class PersonRestaurantDTO
    {
        public int ID { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public bool HasVisited { get; set; }
        public int Priority { get; set; }
        public int Sequence { get; set; }

        public int Rating { get; set; }
        public DateTime LastVisited { get; set; }
        public string Review { get; set; }
        public bool ReviewIsVisible { get; set; }

        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
