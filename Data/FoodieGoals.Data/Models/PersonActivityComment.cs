using System;
using System.Collections.Generic;

namespace FoodieGoals.Data.Models
{
    public class PersonActivityComment
    {
        public int ID { get; set; }
        public virtual PersonActivity Activity { get; set; }
        public virtual Person Commenter { get; set; }
        public string Comment { get; set; }
    }
}