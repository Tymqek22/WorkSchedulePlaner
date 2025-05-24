using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift
{
	public record UpdateShiftCommand(
		int ShiftTileId,
		string Title,
		string? Description,
		List<EmployeeWorkHoursDto> EmployeeWorkHours)
		: ICommand<UpdateShiftResult>;
}
