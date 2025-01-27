using Shop.Web.Models;
using Shop.Web.Services.IServices;

namespace Shop.Web.Services
{
    public class ProductService : BaseSetvice, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductService(IHttpClientFactory clientFactory):base(clientFactory) 
        {
            _clientFactory = clientFactory;
        }
    
        public async Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                ApiData = productDTO,
                ApiUrl = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = SD.ProductAPIBase + "/api/products/"+id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.ProductAPIBase + "/api/products/"+id,
                AccessToken = token
            }); 
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                ApiData = productDTO,
                ApiUrl = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
