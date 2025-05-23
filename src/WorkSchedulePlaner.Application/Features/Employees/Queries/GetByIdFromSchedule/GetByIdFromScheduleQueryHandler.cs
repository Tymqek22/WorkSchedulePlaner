using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Features.Employees.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public class GetByIdFromScheduleQueryHandler : IQueryHandler<GetByIdFromScheduleQuery,EmployeeDto>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public GetByIdFromScheduleQueryHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<EmployeeDto> Handle(
			GetByIdFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employeesFromSchedule = await _employeeRepository
				.GetAsync(e => e.ScheduleId == query.ScheduleId);

			var employee = employeesFromSchedule.FirstOrDefault(e => e.Id == query.EmployeeId);

			return new EmployeeDto
			{
				Id = employee.Id,
				Name = employee.Name,
				LastName = employee.LastName,
				Position = employee.Position,
				ScheduleId = employee.ScheduleId
			};
		}
	}
}
