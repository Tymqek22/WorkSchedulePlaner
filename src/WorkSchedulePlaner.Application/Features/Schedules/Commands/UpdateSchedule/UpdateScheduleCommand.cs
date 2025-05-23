using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule
{
	public record UpdateScheduleCommand(int Id,string Title)
		: ICommand<UpdateScheduleResult>;
}
