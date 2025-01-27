using System.ComponentModel.DataAnnotations;

namespace Shop.Services.ProductAPI.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
    }
}
