using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.OrderAPI.Models;
using Shop.Services.OrderAPI.Models.DTO;
using Shop.Services.OrderAPI.Repositoty;

namespace Shop.Services.OrderAPI.Controllers
{
    //[Authorize]
    [Route("api/orders")]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IOrderRepository _orderRepository;

        public OrderAPIController(IOrderRepository orderRepository, ResponseDTO response)
        {
            _orderRepository = orderRepository;
            this._response = new ResponseDTO();
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<object> GetAll()
        {
            try
            {
                IEnumerable<OrderDTO> orderDTOs = await _orderRepository.GetOrders();
                     _response.Result = orderDTOs;
            }
            catch (Exception ex)
            {
_response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        //[Authorize]
        [Route("{id}")]
        public async Task<object> GetById(string id)
        {
            try
            {
                //OrderDetailsDTO orderDetailsDTOs = await _orderRepository.GetOrderByUserId(id);
                //_response.Result = orderDetailsDTOs;
                IEnumerable<OrderDTO> orderDTOs = await _orderRepository.GetOrders();
                _response.Result = orderDTOs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        public async Task<object> Post([FromBody] OrderDTO orderDTO)
        {
            try
            {
                OrderDTO model = await _orderRepository.CreateUpdateStatusOrders(orderDTO);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        public async Task<object> Put([FromBody] OrderDTO orderDTO)
        {
            try
            {
                OrderDTO model = await _orderRepository.CreateUpdateStatusOrders(orderDTO);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        //[Authorize(Roles ="Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _orderRepository.DeleteOrder(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}