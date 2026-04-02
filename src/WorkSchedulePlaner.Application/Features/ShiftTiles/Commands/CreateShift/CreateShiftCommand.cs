using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift
{
	public record CreateShiftCommand(
		string Title,
		string? Description,
		List<EmployeeWorkHoursDto> EmployeeWorkHours,
		int ScheduleId)
		: ICommand<Result>;
}
