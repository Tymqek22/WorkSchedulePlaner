using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IShiftTileRepository : IRepository<ShiftTile>
	{
		Task<ShiftTile> GetByIdWithAllIncludes(int id);
		Task<IEnumerable<ShiftTile>> GetShiftTilesFromPeriod(DateTime startDate,DateTime endDate);
	}
}
