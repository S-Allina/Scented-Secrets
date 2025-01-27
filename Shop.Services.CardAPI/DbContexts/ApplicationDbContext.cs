using Microsoft.EntityFrameworkCore;
using Shop.Services.CartAPI.Models;

namespace Shop.Services.CartAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
