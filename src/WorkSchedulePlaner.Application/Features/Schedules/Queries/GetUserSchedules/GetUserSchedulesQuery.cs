using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetUserSchedules
{
	public record GetUserSchedulesQuery(string UserId)
		: IQuery<List<WorkScheduleDto>>;
}
