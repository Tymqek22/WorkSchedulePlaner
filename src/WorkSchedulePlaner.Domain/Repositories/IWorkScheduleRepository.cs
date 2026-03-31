using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Domain.Repositories
{
	public interface IWorkScheduleRepository : IRepository<WorkSchedule>
	{
		Task<WorkSchedule> GetWithIncludesAsync(int id);
		Task<WorkSchedule> GetScheduleDetailsFromPeriod(int id,DateTime startDate,DateTime endDate);
		Task<IEnumerable<WorkSchedule>> GetAllUserSchedules(string userId);
	}
}
