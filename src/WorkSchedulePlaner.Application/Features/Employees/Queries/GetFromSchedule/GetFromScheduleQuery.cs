using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Employees.DTOs;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public record GetFromScheduleQuery(int ScheduleId) 
		: IQuery<List<EmployeeDto>>;
}
