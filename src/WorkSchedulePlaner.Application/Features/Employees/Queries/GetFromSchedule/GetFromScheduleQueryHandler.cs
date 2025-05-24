using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public class GetFromScheduleQueryHandler : IQueryHandler<GetFromScheduleQuery,List<EmployeeDto>>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public GetFromScheduleQueryHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<List<EmployeeDto>> Handle(
			GetFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employees = await _employeeRepository.GetAsync(e => e.ScheduleId == query.ScheduleId);

			return employees
				.Select(e => new EmployeeDto
				{
					Id = e.Id,
					Name = e.Name,
					LastName = e.LastName,
					Position = e.Position,
					ScheduleId = e.ScheduleId
				})
				.ToList();
		}
	}
}
