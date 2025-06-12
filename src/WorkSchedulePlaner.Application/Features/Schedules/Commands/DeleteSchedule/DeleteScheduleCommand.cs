using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule
{
	public record DeleteScheduleCommand(int Id)
		: ICommand<Result>;
}
