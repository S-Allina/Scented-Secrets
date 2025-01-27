using Microsoft.EntityFrameworkCore;
using Shop.Services.ProductAPI.Models;

namespace Shop.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                ProductName = "Lacoste Eau De Lacoste L.12.12. Blanc",
                ProductPrice = 760,
                ProductDescription = "Магнетический аромат со свежими нотами",
                CategoryName = "Мужской парфюм",
                ProductImg = "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/1/3/1349097256_1_.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
   {
            ProductId= 3,
    ProductName= "Chanel Coco Mademoiselle",
    ProductPrice= 900,
    ProductDescription= "Изысканный женский аромат с нотами цветов и цитрусовых",
    CategoryName= "Женский парфюм",
    ProductImg= "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/1/4/147565_1_.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
             ProductId= 4,
    ProductName= "Dolce&Gabbana Dolce Peonye",
    ProductPrice= 950,
    ProductDescription= "Свежий и яркий аромат для женщин",
    CategoryName= "Женский парфюм",
    ProductImg= "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/d/o/dolce___gabbana_dolce_peony-_1_1__1_.jpg"
            });


        }

    }
}
