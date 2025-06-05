using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class ScheduleUserRepository : Repository<ScheduleUser>, IScheduleUserRepository
	{
		public ScheduleUserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task DeleteAsyncByIds(string userId,int scheduleId)
		{
			var scheduleUserToDelete = await this.GetByMultipleIdsAsync(userId,scheduleId);

			_dbSet.Remove(scheduleUserToDelete);
		}

		public async Task<ScheduleUser> GetByMultipleIdsAsync(string userId,int scheduleId)
		{
			return await _dbSet.
				FirstOrDefaultAsync(su => su.UserId == userId && su.ScheduleId == scheduleId);
		}

		public async Task<string> GetUserRoleInSchedule(string userId,int scheduleId)
		{
			var scheduleUser = await this.GetByMultipleIdsAsync(userId,scheduleId);

			return scheduleUser.Role;
		}
	}
}
