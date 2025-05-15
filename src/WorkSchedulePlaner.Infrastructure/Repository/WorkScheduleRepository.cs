using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class WorkScheduleRepository : Repository<WorkSchedule>, IWorkScheduleRepository
	{
		public WorkScheduleRepository(ApplicationDbContext dbContext) : base(dbContext) {}

		public async Task<WorkSchedule> GetWithIncludesAsync(int id)
		{
			return await _dbSet
				.Include(ws => ws.ShiftTiles)
				.ThenInclude(st => st.EmployeeShifts)
				.ThenInclude(e => e.Employee)
				.FirstOrDefaultAsync(ws => ws.Id == id);
		}
	}
}
