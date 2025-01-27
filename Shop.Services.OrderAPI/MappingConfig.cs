using AutoMapper;
using Shop.Services.OrderAPI.Models;
using Shop.Services.OrderAPI.Models.DTO;

namespace Shop.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
                config.CreateMap<OrderDTO, Order>().ReverseMap();
                config.CreateMap<CartDetails,CartDetailsDTO>().ReverseMap();
                config.CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();


            });
            return mappingConfig;
        }
    }
}
