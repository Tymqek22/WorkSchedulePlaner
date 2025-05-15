using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public class GetFromScheduleQueryHandler : IQueryHandler<GetFromScheduleQuery,List<Employee>>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public GetFromScheduleQueryHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<List<Employee>> Handle(
			GetFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employees = await _employeeRepository.GetAsync(e => e.ScheduleId == query.ScheduleId);

			return employees.ToList();
		}
	}
}
