using Shop.Services.CouponAPI.Models.DTO;

namespace Shop.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
       Task<CouponDTO> GetCouponByCode(string CouponCode);
      
    }
}
