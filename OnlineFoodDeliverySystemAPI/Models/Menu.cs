namespace OnlineFoodDeliverySystemAPI.Models
{
    public class Menu
    {
        public int MenuId { get; set; } 
        public int RestaurantId { get; set; } 
        public string DishName { get; set; }
        public decimal Price { get; set; }
    }

}
