using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Services.OrderAPI.Models.DTO
{
    public class CartDetailsDTO
    {
        public int CartDetailsId { get; set; }  
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public int OrderDetailsId { get; set; }
        public virtual OrderDetailsDTO OrderDetails { get; set; }
        public int Count { get; set; }  
    }
}
