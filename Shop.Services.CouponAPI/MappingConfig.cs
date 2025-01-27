using AutoMapper;
using Shop.Services.CouponAPI.Models.DTO;
using Shop.Services.CouponAPI.Models;
namespace Shop.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
