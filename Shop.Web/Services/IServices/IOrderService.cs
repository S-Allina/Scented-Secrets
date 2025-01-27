using Shop.Web.Models;

namespace Shop.Web.Services.IServices
{
    public interface IOrderService
    {
        Task<T> UpdateStatusOrders<T>(OrderDetailsDTO orderDetailsDTO, string token = null);
        Task<T> GetOrderByUserIdAsync<T>(string userId, string token = null);
        //Task<T> AddToOrderAsync<T>(OrderDetailsDTO orderDetailsDTO, string token = null);
        Task<T> AddOrderAsync<T>(OrderDTO orderDTO, string token = null);
        Task<T> DeleteOrderAsync<T>(int orderId, string token = null);
        Task<T> GetOrders<T>( string token = null);


    }
}
