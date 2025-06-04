using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkSchedulePlaner.Infrastructure.Identity.Models;

namespace WorkSchedulePlaner.Infrastructure.Identity.DbInitializer
{
	public static class DbInitializer
	{
		public static async Task SeedRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			string[] roles = { "Admin","Employee" };

			foreach (var role in roles) {

				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}

			var adminEmail = "admin@example.com";
			var adminPassword = "Admin123!";

			var adminUser = await userManager.FindByEmailAsync(adminEmail);

			if (adminUser is null) {

				adminUser = new ApplicationUser
				{
					Name = adminEmail,
					LastName = adminEmail,
					BirthDate = new DateTime(2000,01,01),
					UserName = adminEmail,
					Email = adminEmail,
					EmailConfirmed = true
				};

				await userManager.CreateAsync(adminUser);
				await userManager.AddPasswordAsync(adminUser,adminPassword);
				await userManager.AddToRoleAsync(adminUser,"Admin");
			}
		}
	}
}
