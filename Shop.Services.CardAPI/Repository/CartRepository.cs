using Shop.Services.CartAPI.Models.DTO;
using Shop.Services.CartAPI.DbContexts;
using AutoMapper;
using Shop.Services.CartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            try
            {
                var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
                cartFromDb.CouponCode = couponCode;
                _db.CartHeader.Update(cartFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails
                    .RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeader.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
        {
            Cart cart = _mapper.Map<Cart>(cartDTO);
            //check if product exists in database, if not create it
            var prodInDb = await _db.Products.FirstOrDefaultAsync(u =>
            u.ProductId == cartDTO.CartDetails.FirstOrDefault().ProductId);
            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }
            //check if header is null
            var cartHeaderFromDb = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeaderFromDb == null)
            {
                //create header and details
                //_db.CartHeader.Add(cart.CartHeader);
                //await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if header is not null
                //check if details has same product
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                if (cartDetailsFromDb == null)
                {
                    //create details
                    cart.CartDetails.FirstOrDefault().CartDetailsId = 0;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().CartHeader = null;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // update the count
                    //cart.CartDetails.FirstOrDefault().Product = null;
                    //cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cartDetailsFromDb.Count += cart.CartDetails.FirstOrDefault().Count;
                    _db.CartDetails.Update(cartDetailsFromDb);
                    await _db.SaveChangesAsync();

                }
            }

            return _mapper.Map<CartDTO>(cart);

        }

        public async Task<CartDTO> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)

            };
            cart.CartDetails = _db.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u => u.Product);
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            try
            {
                var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
                cartFromDb.CouponCode = "";
                _db.CartHeader.Update(cartFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = _db.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartDetailsId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeader
                         .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeader.Remove(cartHeaderToRemove);

                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
