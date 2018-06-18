using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class PersonFriend
    {
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person Friend { get; set; }
    }
}
