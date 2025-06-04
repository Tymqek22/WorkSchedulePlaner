using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class WorkScheduleRepository : Repository<WorkSchedule>, IWorkScheduleRepository
	{
		public WorkScheduleRepository(ApplicationDbContext dbContext) : base(dbContext) {}

		public async Task<IEnumerable<WorkSchedule>> GetAllUserSchedules(string userId)
		{
			return await _dbSet
				.Include(ws => ws.ShiftTiles)
				.ThenInclude(st => st.EmployeeShifts)
				.ThenInclude(es => es.Employee)
				.Include(ws => ws.UsersInSchedule)
				.Where(ws => ws.OwnerId == userId || ws.UsersInSchedule.Any(su => su.UserId == userId))
				.ToListAsync();
		}

		public async Task<WorkSchedule> GetScheduleDetailsFromPeriod(
			int id,
			DateTime startDate,
			DateTime endDate)
		{
			return await _dbSet
				.Where(ws => ws.Id == id)
				.Include(ws => ws.ShiftTiles.Where(st => st.Date >= startDate && st.Date <= endDate))
				.ThenInclude(st => st.EmployeeShifts)
				.ThenInclude(e => e.Employee)
				.FirstOrDefaultAsync();
		}

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
