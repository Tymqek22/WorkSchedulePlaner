using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Features.Schedules.DTOs;

namespace WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById
{
	public record GetScheduleByIdQuery(int Id)
		: IQuery<WorkScheduleDto>;
}
