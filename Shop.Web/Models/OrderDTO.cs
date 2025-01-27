namespace Shop.Web.Models
{
    public class OrderDTO
    {
        public OrderDetailsDTO orderDetails { get; set; }
        public IEnumerable<CartDetailsDTO> cartDetails { get; set; }
    }
}
