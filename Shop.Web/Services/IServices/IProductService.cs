using Shop.Web.Models;

namespace Shop.Web.Services.IServices
{
    public interface IProductService:IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token = null);
        Task<T> GetProductByIdAsync<T>(int id, string token = null);
        Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token = null);
        Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token = null);
        Task<T> DeleteProductAsync<T>(int id, string token = null);
    }
}
