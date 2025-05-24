using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift
{
	public record AssignShiftCommand(
		string Title,
		string? Description,
		List<EmployeeWorkHoursDto> EmployeeWorkHours,
		DateTime Date,
		int ScheduleId)
		: ICommand<AssignShiftResult>;
}
