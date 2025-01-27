using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Services.ProductAPI.DbContexts;
using Shop.Services.ProductAPI.Models;
using Shop.Services.ProductAPI.Models.DTO;

namespace Shop.Services.ProductAPI.Repositoty
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db= db;
            _mapper= mapper;
        }
        public async Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO)
        {
            Product product =_mapper.Map<ProductDTO,Product>(productDTO);
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
           await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
                if (product == null) { 
                    return false; 
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
           Product product = await _db.Products.Where(x=>x.ProductId==id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
