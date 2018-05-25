using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class PersonList
    {
        public int ID { get; set; }
        public virtual Person Owner { get; set; }

        public string Title { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<ListRestaurant> ListRestaurants { get; set; }
        public virtual Photo CoverPhoto { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
