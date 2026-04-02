using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) {}
	}
}
