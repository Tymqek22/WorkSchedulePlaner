using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IWorkScheduleRepository : IRepository<WorkSchedule>
	{
		Task<WorkSchedule> GetWithIncludesAsync(int id);
		Task<WorkSchedule> GetScheduleDetailsFromPeriod(int id,DateTime startDate,DateTime endDate);
	}
}
