using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Domain.Repositories
{
	public interface IShiftTileRepository : IRepository<ShiftTile>
	{
		Task<ShiftTile> GetByIdWithAllIncludes(int id);
		Task<IEnumerable<ShiftTile>> GetShiftTilesFromPeriod(DateTime startDate,DateTime endDate);
	}
}
