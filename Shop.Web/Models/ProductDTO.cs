using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Count = 1;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        [Range(0, 100)]
        public int Count { get; set; }  
    }
}
