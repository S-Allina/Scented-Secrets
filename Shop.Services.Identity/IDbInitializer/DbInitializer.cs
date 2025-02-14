﻿using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shop.Services.Identity.DbContexts;
using Shop.Services.Identity.Models;
using System.Security.Claims;

namespace Shop.Services.Identity.IDbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}
	
		public void Initialize()
		{
			//if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
			//{
			//	_roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
			//	_roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
			//}
			//else
			//{
			//	return;
			//}
			ApplicationUser adminUser = new()
			{
				UserName = "admin1@gmail.com",
				Email = "admin1@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "11111111111",
				NormalizedUserName = "Alina admin",

			};
			//_userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
			//_userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

			//var claims1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
			//{
			//	new Claim(JwtClaimTypes.Name,adminUser.NormalizedUserName),
			//	new Claim(JwtClaimTypes.Role,SD.Admin)
				
			//}).Result;

			ApplicationUser customerUser = new()
			{
				UserName = "customer1@gmail.com",
				Email = "customer1@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "11111111111",
				NormalizedUserName = "Anna Customer",

			};
			//_userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
			//_userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

			//var temp1 = _userManager.AddClaimsAsync(customerUser, new Claim[]
			//{
			//	new Claim(JwtClaimTypes.Name,customerUser.NormalizedUserName),
			//	new Claim(JwtClaimTypes.Role,SD.Customer)

			//}).Result;
		}
	}
}
