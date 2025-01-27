namespace Shop.Services.OrderAPI.Models
{
    public class Order
    {
        public OrderDetails orderDetails { get; set; }
        public IEnumerable<CartDetails> cartDetails { get; set; }
    }
}
