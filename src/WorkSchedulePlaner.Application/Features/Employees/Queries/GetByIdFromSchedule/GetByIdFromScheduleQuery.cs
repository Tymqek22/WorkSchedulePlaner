using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule
{
	public record GetByIdFromScheduleQuery(int ScheduleId, int EmployeeId)
		: IQuery<Employee>;
}
