using WorkSchedulePlaner.Application.DTO;

namespace WorkSchedulePlaner.Application.ShiftTiles.UpdateShift
{
	public record UpdateShiftRequest(
		int ShiftTileId,
		List<EmployeeWorkHoursDto> EmployeeWorkHours,
		string Title,
		string? Description);

}
