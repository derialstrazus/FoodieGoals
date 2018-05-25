namespace FoodieGoals.Data.Models
{
    public class PersonPhoto
    {
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Photo Photo { get; set; }
    }
}