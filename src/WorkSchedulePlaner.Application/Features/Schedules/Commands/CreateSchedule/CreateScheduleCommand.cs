using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule
{
	public record CreateScheduleCommand(string Title,string OwnerId)
		: ICommand<Result>;
}
