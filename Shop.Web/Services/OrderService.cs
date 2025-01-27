using NuGet.Common;
using Shop.Web.Models;
using Shop.Web.Services.IServices;

namespace Shop.Web.Services
{
    public class OrderService : BaseSetvice, IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;
        public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> AddOrderAsync<T>(OrderDTO orderDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                ApiData = orderDTO,
                ApiUrl = SD.OrderAPIBase + "/api/orders/Post",
                AccessToken = token
            });
        }

        public async Task<T> DeleteOrderAsync<T>(int orderId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = SD.OrderAPIBase + "/api/orders/Delete" + orderId,
                AccessToken = token
            });
        }

      

        public async Task<T> GetOrderByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.OrderAPIBase + "/api/orders/GetById/" + userId,
                AccessToken = token
            });
        }

        public async Task<T> GetOrders<T>(string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.OrderAPIBase + "/api/orders/GetAll",
                AccessToken = token
            });
        }



        public async Task<T> UpdateStatusOrders<T>(OrderDetailsDTO orderDetailsDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                ApiData = orderDetailsDTO,
                ApiUrl = SD.OrderAPIBase + "/api/orders/put",
                AccessToken = token
            });
        }

    }
}
