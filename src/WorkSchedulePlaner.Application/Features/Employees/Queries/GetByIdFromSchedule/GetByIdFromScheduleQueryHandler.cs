using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public class GetByIdFromScheduleQueryHandler : IQueryHandler<GetByIdFromScheduleQuery,Employee>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public GetByIdFromScheduleQueryHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<Employee> Handle(
			GetByIdFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employeesFromSchedule = await _employeeRepository
				.GetAsync(e => e.ScheduleId == query.ScheduleId);

			return employeesFromSchedule.FirstOrDefault(e => e.Id == query.EmployeeId);
		}
	}
}
