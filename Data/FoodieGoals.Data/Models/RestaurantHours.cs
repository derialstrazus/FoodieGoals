using System;
using System.Collections.Generic;

namespace FoodieGoals.Data.Models
{
    public class RestaurantHours
    {
        public int ID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public int DayWeek { get; set; }
        public string Schedule { get; set; }    //Just let the front parse this
        //public TimeSpan OpenHours { get; set; }
        //public TimeSpan FirstBreakStart { get; set; }
        //public TimeSpan FirstBreakEnd { get; set; }
        //public TimeSpan SecondBreakStart { get; set; }
        //public TimeSpan SecondBreakEnd { get; set; }
        //public TimeSpan CloseHours { get; set; }
    }
}