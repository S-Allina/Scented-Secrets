using AutoMapper;
using Shop.Services.CartAPI.Models;
using Shop.Services.CartAPI.Models.DTO;

namespace Shop.Services.CartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();

                config.CreateMap<CartDetailsDTO, CartDetails>().ReverseMap();

                config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();

                config.CreateMap<CartDTO, Cart>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
