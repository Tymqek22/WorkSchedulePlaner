using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod
{
	public record GetScheduleDetailsFromPeriodQuery(
		int Id,
		DateOnly StartDate,
		DateOnly EndDate)
		: IQuery<WorkScheduleDto>;
}
