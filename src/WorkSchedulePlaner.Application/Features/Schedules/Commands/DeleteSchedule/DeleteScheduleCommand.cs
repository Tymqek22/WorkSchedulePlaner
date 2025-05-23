using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule
{
	public record DeleteScheduleCommand(int Id)
		: ICommand<DeleteScheduleResult>;
}
