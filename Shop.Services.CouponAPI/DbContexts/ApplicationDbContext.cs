using Microsoft.EntityFrameworkCore;
using Shop.Services.CouponAPI.Models;

namespace Shop.Services.CouponAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(new
                Coupon
            {
                CouponId = 1,
                CouponCode = "15OFF",
                DiscountAmount = 15,
            });
            modelBuilder.Entity<Coupon>().HasData(new
               Coupon
            {
                CouponId = 2,
                CouponCode = "100OFF",
                DiscountAmount = 100,
            });

        }
    }
}
