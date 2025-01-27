using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Web.Models;
using Shop.Web.Services.IServices;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDTO>("");
            if (response!=null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDTO model)
        {
            if (ModelState.IsValid) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProductAsync<ResponseDTO>(model, accessToken);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(ProductIndex));
                        }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId, "");
            if (response != null && response.IsSuccess == true)
            {
              ProductDTO  product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(product);

            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProductAsync<ResponseDTO>(model, accessToken);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId, accessToken);
            if (response != null && response.IsSuccess == true)
            {
                ProductDTO product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(product);

            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDTO model)
        {
            //if (ModelState.IsValid)
            //{
                var response = await _productService.DeleteProductAsync<ResponseDTO>(model.ProductId,"");
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            //}
            return View(model);
        }
    }
}
