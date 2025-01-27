using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Web.Models;
using Shop.Web.Services.IServices;
using System.Diagnostics;

namespace Shop.Web.Controllers
{

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;


        public HomeController(ILogger<HomeController> logger, IProductService productService,ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> products = new();
            var response = await _productService.GetAllProductsAsync<ResponseDTO>();
            if(response != null && response.IsSuccess) {
                products = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));

            }
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            ProductDTO product = new();
            var response = await _productService.GetProductByIdAsync<ResponseDTO>(id);
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

            }
            return View(product);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductDTO productDTO)
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
                CartDetailsDTO cartDetails = new()
                {
                    Count = productDTO.Count,
                    ProductId = productDTO.ProductId,
                    CartHeader = cartDTO.CartHeader

                };
                var resp = await _productService.GetProductByIdAsync<ResponseDTO>(productDTO.ProductId, "");
                if (resp != null && resp.IsSuccess == true)
                {
                    cartDetails.CartHeaderId = cartDTO.CartHeader.CartHeaderId;
                    cartDetails.CartDetailsId = cartDTO.CartDetails.FirstOrDefault() ==null ? 0 : cartDTO.CartDetails.FirstOrDefault().CartDetailsId;
                    cartDetails.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(resp.Result));
                    cartDetails.ProductId = productDTO.ProductId;
                   
                }
                List<CartDetailsDTO> cartDetailsDTOs = new();
                cartDetailsDTOs.Add(cartDetails);
                cartDTO.CartDetails = cartDetailsDTOs;
               var responseUpdate = _cartService.AddToCartAsync<ResponseDTO>(cartDTO, accessToken);
                if (responseUpdate.Result != null)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                cartDTO.CartHeader = new CartHeaderDTO
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value,
                    CouponCode = ""
                };
                
            }
            
            
            return View(productDTO);
        }
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies","oidc");
        }
    }
}