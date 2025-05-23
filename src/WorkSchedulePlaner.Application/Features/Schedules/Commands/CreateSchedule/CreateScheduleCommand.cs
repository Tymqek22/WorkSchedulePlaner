using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule
{
	public record CreateScheduleCommand(string Title)
		: ICommand<CreateScheduleResult>;
}
