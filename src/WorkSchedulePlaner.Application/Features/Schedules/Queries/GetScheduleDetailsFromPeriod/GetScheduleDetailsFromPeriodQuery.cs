using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod
{
	public record GetScheduleDetailsFromPeriodQuery(
		int Id,
		DateTime StartDate,
		DateTime EndDate)
		: IQuery<WorkScheduleDto>;
}
