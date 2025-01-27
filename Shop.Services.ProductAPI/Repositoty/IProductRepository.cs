using Shop.Services.ProductAPI.Models.DTO;

namespace Shop.Services.ProductAPI.Repositoty
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO);
        Task<bool> DeleteProduct(int id);
    }
}
