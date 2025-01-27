using AutoMapper;
using Shop.Services.ProductAPI.Models;
using Shop.Services.ProductAPI.Models.DTO;

namespace Shop.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<Product, ProductDTO>();

            });
            return mappingConfig;
        }
    }
}
