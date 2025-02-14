﻿using Shop.Services.CartAPI.Models.DTO;

namespace Shop.Services.CartAPI.Repository
{
    public interface ICartRepository
    {
       Task<CartDTO> GetCartByUserId(string userId);
        Task<CartDTO> CreateUpdateCart(CartDTO cartDTO);
       Task<bool>  RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
        Task<bool> ApplyCoupon(string userId,string couponCode);
        Task<bool> RemoveCoupon(string userId);


    }

}
