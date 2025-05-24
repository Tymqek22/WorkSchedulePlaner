using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule
{
	public record GetFromScheduleQuery(int ScheduleId) 
		: IQuery<List<EmployeeDto>>;
}
