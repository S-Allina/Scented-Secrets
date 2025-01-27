using Shop.Web.Models;
using Shop.Web.Services.IServices;

namespace Shop.Web.Services
{
    public class CouponService : BaseSetvice, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
     

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }

      
    }
}
