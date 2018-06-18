using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class PersonRestaurantHistory
    {
        public int ID { get; set; }
        public virtual PersonRestaurant PersonRestaurant { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
