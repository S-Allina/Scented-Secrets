using Shop.Services.OrderAPI.Models;
using Shop.Services.OrderAPI.Models.DTO;

namespace Shop.Services.OrderAPI.Repositoty
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> GetOrderByUserId(string id);
        Task<OrderDTO> CreateUpdateStatusOrders(OrderDTO orderDetailsDTO);

        Task<bool> DeleteOrder(int id);
    }
}
