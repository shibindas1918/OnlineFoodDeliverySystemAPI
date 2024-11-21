namespace OnlineFoodDeliverySystemAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; } 
        public string CustomerName { get; set; }
        public int RestaurantId { get; set; } 
        public decimal TotalPrice { get; set; }
        public string OrderDetails { get; set; }
    }
}
