using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IScheduleUserRepository : IRepository<ScheduleUser>
	{
		Task<ScheduleUser> GetByMultipleIdsAsync(string userId,int scheduleId);
		Task DeleteAsyncByIds(string userId,int scheduleId);
	}
}
