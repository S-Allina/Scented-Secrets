using Microsoft.AspNetCore.Mvc;
using Shop.Services.CartAPI.Messages;
using Shop.Services.CartAPI.Models.DTO;
using Shop.Services.CartAPI.Repository;

namespace Shop.Services.CartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController:Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDTO _response;
        public CartAPIController(ICartRepository cartRepository, ResponseDTO response)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDTO();
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserId(userId);
                _response.Result= cartDTO;
            }catch(Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart( CartDTO cartDTO)
        {
            try
            {
                CartDTO cartDTO2 = await _cartRepository.CreateUpdateCart(cartDTO);
                _response.Result = cartDTO2;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO cartDTO)
        {
            try
            {
                CartDTO cartDTO2 = await _cartRepository.CreateUpdateCart(cartDTO);
                _response.Result = cartDTO2;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("DeleteCart")]
        public async Task<object> DeleteCart([FromBody]int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId,cartDTO.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("ClearCart")]
        public async Task<object> ClearCart([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.ClearCart(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout([FromBody] CheckoutHeaderDto checkoutHeader)
        {
            try
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserId(checkoutHeader.UserId);
                if(cartDTO == null)
                {
                    return BadRequest();
                }
                checkoutHeader.CartDetails = cartDTO.CartDetails;
 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }

}
