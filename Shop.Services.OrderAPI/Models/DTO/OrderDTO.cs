namespace Shop.Services.OrderAPI.Models.DTO
{
    public class OrderDTO
    {
        public OrderDetailsDTO orderDetails { get; set; }
        public IEnumerable<CartDetailsDTO> cartDetails { get; set; }
    }
}
