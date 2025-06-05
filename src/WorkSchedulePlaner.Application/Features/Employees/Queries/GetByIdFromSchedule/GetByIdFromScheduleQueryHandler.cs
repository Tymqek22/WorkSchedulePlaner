using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public class GetByIdFromScheduleQueryHandler : IQueryHandler<GetByIdFromScheduleQuery,EmployeeDto>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IIdentityService _identityService;

		public GetByIdFromScheduleQueryHandler(
			IRepository<Employee> employeeRepository,
			IIdentityService identityService)
		{
			_employeeRepository = employeeRepository;
			_identityService = identityService;
		}

		public async Task<EmployeeDto> Handle(
			GetByIdFromScheduleQuery query,
			CancellationToken cancellationToken = default)
		{
			var employeesFromSchedule = await _employeeRepository
				.GetAsync(e => e.ScheduleId == query.ScheduleId);

			var employee = employeesFromSchedule.FirstOrDefault(e => e.Id == query.EmployeeId);

			var email = await _identityService.GetUserEmailById(employee.UserId);

			return employee.MapToDtoWithEmail(email);
		}
	}
}
