using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Services.OrderAPI.Models
{
    public class CartDetails
    {
        public int CartDetailsId { get; set; }  
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int OrderDetailsId { get; set; }
        [ForeignKey("OrderDetailsId")]
        public virtual OrderDetails OrderDetails { get; set; }
        public int Count { get; set; }  
    }
}
