
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Services.Identity.DbContexts;
using Shop.Services.Identity.IDbInitializer;
using Shop.Services.Identity.Models;

namespace Shop.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //  .AddEntityFrameworkStores<ApplicationDbContext>();
        
           
            //   static IHostBuilder CreateHostBuilder(string[] args) =>
            //Host.CreateDefaultBuilder(args)
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseUrls("https://localhost:5151"); // Установите используемый порт и URL Identity Server

            //    });

    



            //        builderIdentity.AddDeveloperSigningCredential();


            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			builder.Services.AddScoped<IDbInitializer.IDbInitializer, DbInitializer>();

			builder.Services.AddRazorPages();
		 builder.Services.AddIdentityServer(options =>
                     {
                         options.Events.RaiseErrorEvents = true;
                         options.Events.RaiseInformationEvents = true;
                         options.Events.RaiseSuccessEvents = true;
                         options.Events.RaiseFailureEvents = true;
                         options.EmitStaticAudienceClaim = true;
                         
                     })
                .AddInMemoryIdentityResources(SD.IdentityResources)
                     .AddInMemoryApiScopes(SD.apiScopes)
                     .AddInMemoryClients(SD.Clients)
                   .AddAspNetIdentity<ApplicationUser>()
                   .AddDeveloperSigningCredential();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            SeedDataBase();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages().RequireAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

            void SeedDataBase()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer.IDbInitializer>();
                    dbInitializer.Initialize();
                }
            }
        }
    }
}