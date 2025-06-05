using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Infrastructure.Identity.Models;

namespace WorkSchedulePlaner.Infrastructure.Identity.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public IdentityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<string> GetUserEmailById(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user is not null)
				return user.Email;

			return null;
		}

		public async Task<string> GetUserIdByEmail(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user is not null)
				return user.Id;

			return null;
		}
	}
}
