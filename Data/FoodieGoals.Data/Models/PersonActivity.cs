using System;
using System.Collections.Generic;

namespace FoodieGoals.Data.Models
{
    public class PersonActivity
    {
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public string Activity { get; set; }
        public virtual ICollection<PersonActivityComment> Comments { get; set; }
    }
}