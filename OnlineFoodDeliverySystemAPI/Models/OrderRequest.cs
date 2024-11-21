namespace OnlineFoodDeliverySystemAPI.Models
{
    public class OrderRequest
    {
        public string CustomerName { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Dish> OrderDetails { get; set; }
    }

    public class Dish
    {
        public string DishName { get; set; }
        public decimal Price { get; set; }
    }

}
