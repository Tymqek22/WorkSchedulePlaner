using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule
{
	public record UpdateScheduleCommand(int Id,string Title)
		: ICommand<Result>;
}
