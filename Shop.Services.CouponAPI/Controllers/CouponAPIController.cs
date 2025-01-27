using Microsoft.AspNetCore.Mvc;
using Shop.Services.CouponAPI.Models.DTO;
using Shop.Services.CouponAPI.Repository;

namespace Shop.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        protected ResponseDTO _response;
        public CouponAPIController(ICouponRepository couponRepository, ResponseDTO response)
        {
            _couponRepository = couponRepository;
            this._response = new ResponseDTO();
        }
        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try
            {
                CouponDTO couponDTO = await _couponRepository.GetCouponByCode(code);
                _response.Result = couponDTO;
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
