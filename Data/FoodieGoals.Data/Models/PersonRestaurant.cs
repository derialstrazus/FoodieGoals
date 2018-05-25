using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class PersonRestaurant
    {
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public bool HasVisited { get; set; }
        public int Priority { get; set; }

        public int Rating { get; set; }
        public DateTime LastVisited { get; set; }
        public string Review { get; set; }
        public bool ReviewIsVisible { get; set; }

        public string Notes { get; set; }   

        public virtual ICollection<PersonRestaurantPhoto> PersonRestaurantPhotos { get; set; }
        public virtual ICollection<ListRestaurant> ListRestaurants { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
