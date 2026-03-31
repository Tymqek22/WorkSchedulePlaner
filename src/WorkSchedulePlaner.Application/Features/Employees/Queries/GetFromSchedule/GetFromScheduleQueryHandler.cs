using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public class GetFromScheduleQueryHandler : IQueryHandler<GetFromScheduleQuery,List<EmployeeDto>>
	{
		private readonly IEmployeeRepository _employeeRepository;

		public GetFromScheduleQueryHandler(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<List<EmployeeDto>> Handle(
			GetFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employees = await _employeeRepository.GetAsync(e => e.ScheduleId == query.ScheduleId);

			return employees
				.Select(e => e.MapToDto())
				.ToList();
		}
	}
}
