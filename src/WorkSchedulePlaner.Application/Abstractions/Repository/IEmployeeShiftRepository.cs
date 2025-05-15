using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IEmployeeShiftRepository : IRepository<EmployeeShift>
	{
		public Task DeleteManyAsync(Func<EmployeeShift,bool> predicate);
	}
}
