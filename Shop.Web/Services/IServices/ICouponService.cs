using Shop.Web.Models;

namespace Shop.Web.Services.IServices
{
    public interface ICouponService
    {
      
        Task<T> GetCoupon<T>(string couponToken, string token = null);


    }
}
