using Microsoft.EntityFrameworkCore;
using Shop.Services.OrderAPI.Models;

namespace Shop.Services.OrderAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
