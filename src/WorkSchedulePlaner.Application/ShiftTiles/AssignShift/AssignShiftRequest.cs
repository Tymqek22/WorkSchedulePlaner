using WorkSchedulePlaner.Application.DTO;

namespace WorkSchedulePlaner.Application.ShiftTiles.AssignShift
{
	public record AssignShiftRequest(
		List<EmployeeWorkHoursDto> EmployeeWorkHours,
		string Title,
		string? Description,
		DateTime Date,
		int ScheduleId);
}
