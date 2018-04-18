using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class ListRestaurant
    {
        public int ID { get; set; }
        public virtual PersonList PersonList { get; set; }
        public virtual PersonRestaurant PersonRestaurant { get; set; }

        public string Comment { get; set; }
        public int Sequence { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
