using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Services.CartAPI.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(1, 1000)]
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
    }
}
