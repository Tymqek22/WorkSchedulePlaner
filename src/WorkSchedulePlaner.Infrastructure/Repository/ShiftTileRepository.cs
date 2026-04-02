using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class ShiftTileRepository : Repository<ShiftTile>, IShiftTileRepository
	{
		public ShiftTileRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task<ShiftTile> GetByIdWithAllIncludes(int id)
		{
			return await _dbSet
				.Include(st => st.Assignments)
				.FirstOrDefaultAsync(st => st.Id == id);
		}

		public async Task<IEnumerable<ShiftTile>> GetShiftTilesFromPeriod(DateOnly startDate,DateOnly endDate)
		{
			return await _dbSet
				.Include(st => st.Assignments)
				.Where(st => st.Date >= startDate && st.Date <= endDate)
				.ToListAsync();
		}
	}
}
