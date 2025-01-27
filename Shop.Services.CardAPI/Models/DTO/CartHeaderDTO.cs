using System.ComponentModel.DataAnnotations;

namespace Shop.Services.CartAPI.Models.DTO
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }

    }
}
