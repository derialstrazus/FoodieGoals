namespace FoodieGoals.Data.Models
{
    public class PersonRestaurantPhoto
    {
        public int ID { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual PersonRestaurant PersonRestaurant { get; set; }
    }
}