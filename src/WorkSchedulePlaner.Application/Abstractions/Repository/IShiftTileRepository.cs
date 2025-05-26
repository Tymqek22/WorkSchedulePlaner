using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IShiftTileRepository : IRepository<ShiftTile>
	{
		Task<ShiftTile> GetByIdWithAllIncludes(int id);
	}
}
