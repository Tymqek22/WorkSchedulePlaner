using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Employees.DTOs;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public record GetByIdFromScheduleQuery(int ScheduleId, int EmployeeId)
		: IQuery<EmployeeDto>;
}
