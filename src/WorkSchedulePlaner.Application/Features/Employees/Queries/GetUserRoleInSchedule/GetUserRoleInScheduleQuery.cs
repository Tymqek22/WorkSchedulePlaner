using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Employees.Queries.GetUserRoleInSchedule
{
	public record GetUserRoleInScheduleQuery(string UserId,int ScheduleId)
		: IQuery<string>;
}
