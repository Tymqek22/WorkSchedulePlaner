using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public record GetByIdFromScheduleQuery(int ScheduleId, int EmployeeId)
		: IQuery<EmployeeDto>;
}
