namespace WorkSchedulePlaner.Application.Abstractions.Services
{
	public interface IIdentityService
	{
		Task<string> GetUserIdByEmail(string email);
		Task<string> GetUserEmailById(string userId);
	}
}
