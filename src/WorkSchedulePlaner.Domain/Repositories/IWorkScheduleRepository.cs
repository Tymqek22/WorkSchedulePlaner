using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Domain.Repositories
{
	public interface IWorkScheduleRepository : IRepository<WorkSchedule>
	{
		Task<WorkSchedule> GetByIdWithDetailsAsync(int id);
		Task<WorkSchedule> GetScheduleDetailsFromPeriod(int id,DateOnly startDate,DateOnly endDate);
		Task<IEnumerable<WorkSchedule>> GetAllUserSchedules(string userId);
		Task<Employee> GetEmployeeByIdAsync(int id);
	}
}
