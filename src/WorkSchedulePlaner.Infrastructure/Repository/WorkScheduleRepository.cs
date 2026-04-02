using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;
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
				.Include(es => es.Employees)
				.Where(ws => ws.OwnerId == userId || ws.Employees.Any(su => su.UserId == userId))
				.ToListAsync();
			
		}

		public async Task<WorkSchedule> GetByIdWithDetailsAsync(int id)
		{
			return await _dbSet
				.Include(ws => ws.ShiftTiles)
				.Include(e => e.Employees)
				.FirstOrDefaultAsync(ws => ws.Id == id);
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			//return await _dbContext.Employees
			//	.FirstOrDefaultAsync(e => e.)
			return null;
		}

		public async Task<WorkSchedule> GetScheduleDetailsFromPeriod(
			int id,
			DateOnly startDate,
			DateOnly endDate)
		{
			return await _dbSet
				.Where(ws => ws.Id == id)
				.Include(ws => ws.ShiftTiles.Where(st => st.Date >= startDate && st.Date <= endDate))
				.Include(e => e.Employees)
				.FirstOrDefaultAsync();
		}
	}
}
