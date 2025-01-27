using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.ProductAPI.Models.DTO;
using Shop.Services.ProductAPI.Repositoty;

namespace Shop.Services.ProductAPI.Controllers
{
    //[Authorize]
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository, ResponseDTO response)
        {
            _productRepository = productRepository;
            this._response = new ResponseDTO();
        }
      
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDTO> productDTOs = await _productRepository.GetProducts();
                     _response.Result = productDTOs;
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
        public async Task<object> GetById(int id)
        {
            try
            {
                ProductDTO productDTOs = await _productRepository.GetProductById(id);
                _response.Result = productDTOs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDTO);
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
        public async Task<object> Put([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDTO);
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
        //[Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
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