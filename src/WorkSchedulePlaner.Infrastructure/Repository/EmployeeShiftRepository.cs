using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class EmployeeShiftRepository : Repository<EmployeeShift>, IEmployeeShiftRepository
	{
		public EmployeeShiftRepository(ApplicationDbContext dbContext) : base(dbContext) {}

		public async Task DeleteManyAsync(Func<EmployeeShift,bool> predicate)
		{
			var shiftsToRemove = _dbSet.Where(predicate).ToList();

			_dbSet.RemoveRange(shiftsToRemove);
		}
	}
}
