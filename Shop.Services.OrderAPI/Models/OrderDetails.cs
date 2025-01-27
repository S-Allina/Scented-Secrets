using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Services.OrderAPI.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        //public int CartDetailsId { get; set; }
        public int CartTotalItems { get; set; }

        //[ForeignKey("CartDetailsId")]
        //public IEnumerable<CartDetails> CartDetails { get; set; }

        public string UserId { get; set; }
        public string? CouponCode { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
    }
}
