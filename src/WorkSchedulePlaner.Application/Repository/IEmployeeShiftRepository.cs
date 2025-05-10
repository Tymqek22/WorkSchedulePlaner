using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Repository
{
	public interface IEmployeeShiftRepository : IRepository<EmployeeShift>
	{
		public Task DeleteManyAsync(Func<EmployeeShift,bool> predicate);
		public Task<IEnumerable<EmployeeShift>> GetManyAsync(Func<EmployeeShift,bool> predicate);
	}
}
