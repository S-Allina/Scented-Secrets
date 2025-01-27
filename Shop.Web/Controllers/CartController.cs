using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Shop.Web.Models;
using Shop.Web.Services;
using Shop.Web.Services.IServices;

namespace Shop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService,IOrderService orderService, IProductService productService, ICouponService couponService)
        {
            _orderService= orderService;
            _cartService = cartService;
            _productService = productService;
            _couponService = couponService;
        }
        // GET: CartController
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDTOBasedOnLoggedInUser());
        }
        public async Task<IActionResult> Checkout()
        {
			return View(await LoadCartDTOBasedOnLoggedInUser());
		}
		//[HttpPost]
		//public async Task<IActionResult> Checkout(CartDTO cartDTO)
		//{
		//    try
		//    {
		//        var accessToken = await HttpContext.GetTokenAsync("access_token");
		//        var response = await _cartService.Checkout<ResponseDTO>(cartDTO.CartHeader,accessToken);

		//        return RedirectToAction(nameof(Confirmation));
		//    }
		//    catch (Exception e)
		//    {

		//        return View(cartDTO);
		//    }
		//}
		[HttpPost]
		public async Task<IActionResult> Checkout(CartDTO cartHeaderDTO)
		{
			try
			{
				OrderDTO orderDTO = new OrderDTO();
                orderDTO.orderDetails=new OrderDetailsDTO();
				orderDTO.orderDetails.OrderTotal = cartHeaderDTO.CartHeader.OrderTotal;
				orderDTO.orderDetails.FirstName = cartHeaderDTO.CartHeader.FirstName;
				orderDTO.orderDetails.LastName = cartHeaderDTO.CartHeader.LastName;
				orderDTO.orderDetails.Phone = cartHeaderDTO.CartHeader.Phone;
				orderDTO.orderDetails.Email = cartHeaderDTO.CartHeader.Email;
				orderDTO.orderDetails.PickupDateTime = cartHeaderDTO.CartHeader.PickupDateTime;
				orderDTO.orderDetails.ExpiryMonthYear = cartHeaderDTO.CartHeader.ExpiryMonthYear;
				orderDTO.orderDetails.CardNumber = cartHeaderDTO.CartHeader.CardNumber;
				orderDTO.orderDetails.CVV = cartHeaderDTO.CartHeader.CVV;
				orderDTO.orderDetails.DiscountTotal = cartHeaderDTO.CartHeader.DiscountTotal;
				orderDTO.orderDetails.UserId = cartHeaderDTO.CartHeader.UserId;
				orderDTO.orderDetails.Count = cartHeaderDTO.CartDetails.Count();
				orderDTO.cartDetails= cartHeaderDTO.CartDetails;
				var accessToken = await HttpContext.GetTokenAsync("access_token");
				var response = await _orderService.AddOrderAsync<ResponseDTO>(orderDTO, accessToken);

				return RedirectToAction(nameof(Confirmation));
			}
			catch (Exception e)
			{

				return View(cartHeaderDTO);
			}
		}
		[HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            cartDTO.CartDetails = new List<CartDetailsDTO>();
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCoupon<ResponseDTO>(cartDTO, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCoupon<ResponseDTO>(userId, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        public async Task<IActionResult> CartRemove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View(response);
        }
        public async Task<IActionResult> PlusMinusProductToCart(int cartDetailsid, string option, int productId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);
            CartDTO cartDTO = new();
            if (response != null && response.IsSuccess)
            {
                cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }
            if (cartDTO?.CartDetails.FirstOrDefault(u => u.ProductId == productId)?.CartDetailsId == cartDetailsid)
            {
                CartDetailsDTO cartDetails = new()
                {
                    Count = option == "plus" ? 1 : -1,
                    ProductId = productId,
                    CartHeader = cartDTO.CartHeader

                };
                var resp = await _productService.GetProductByIdAsync<ResponseDTO>(productId, "");
                if (resp != null && resp.IsSuccess == true)
                {
                    cartDetails.CartHeaderId = cartDTO.CartDetails.FirstOrDefault(u => u.ProductId == productId).CartHeaderId;
                    cartDetails.CartHeader.UserId = userId;
                    cartDetails.CartDetailsId = cartDTO.CartDetails.FirstOrDefault(u => u.ProductId == productId).CartDetailsId;
                    cartDetails.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(resp.Result));

                }


                List<CartDetailsDTO> cartDetailsDTOs = new();
                cartDetailsDTOs.Add(cartDetails);
                cartDTO.CartDetails = cartDetailsDTOs;
                cartDTO.CartHeader.CartHeaderId = cartDTO.CartDetails.FirstOrDefault(u => u.ProductId == productId).CartHeaderId;
                //cartDTO.CartDetails.FirstOrDefault(u => u.ProductId == productId).CartHeader.CartHeaderId = 0;

                var responseUpdate = _cartService.UpdateCartAsync<ResponseDTO>(cartDTO, accessToken);
                if (responseUpdate.Result != null)
                {

                    return RedirectToAction(nameof(CartIndex));
                }
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: CartController/Create

        private async Task<CartDTO> LoadCartDTOBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);
            CartDTO cartDTO = new();
            if (response != null && response.IsSuccess)
            {
                cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }
            if (cartDTO.CartHeader != null)
            {
                if(!string.IsNullOrEmpty(cartDTO.CartHeader.CouponCode))
                {
                    var coupon =await _couponService.GetCoupon<ResponseDTO>(cartDTO.CartHeader.CouponCode, accessToken);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                        cartDTO.CartHeader.DiscountTotal = couponObj ==null ? 0 : couponObj.DiscountAmount;
                    }
                }
                foreach (var detail in cartDTO.CartDetails)
                {
                    cartDTO.CartHeader.OrderTotal += (detail.Count * detail.Product.ProductPrice);
                }
                cartDTO.CartHeader.OrderTotal -= cartDTO.CartHeader.DiscountTotal;
            }

            return cartDTO;
        }
    }
}
