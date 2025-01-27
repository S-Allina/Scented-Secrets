using Shop.Services.CouponAPI.Models.DTO;
using Shop.Services.CouponAPI.DbContexts;
using AutoMapper;
using Shop.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string CouponCode)
        {
          var couponFromDb=await _db.Coupons.FirstOrDefaultAsync(c=>c.CouponCode==CouponCode);
        return _mapper.Map<CouponDTO>(couponFromDb);
        }
    }
}
