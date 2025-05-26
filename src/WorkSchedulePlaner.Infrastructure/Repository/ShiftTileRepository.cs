using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class ShiftTileRepository : Repository<ShiftTile>, IShiftTileRepository
	{
		public ShiftTileRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task<ShiftTile> GetByIdWithAllIncludes(int id)
		{
			return await _dbSet
				.Include(st => st.EmployeeShifts)
				.ThenInclude(es => es.Employee)
				.FirstOrDefaultAsync(st => st.Id == id);
		}
	}
}
