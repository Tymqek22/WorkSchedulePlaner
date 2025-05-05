namespace WorkSchedulePlaner.Application.ShiftTiles.AssignShift
{
	public record AssignShiftRequest(
		int UserId,
		string Title,
		string? Description,
		DateTime Date,
		int ScheduleId,
		TimeSpan StartTime,
		TimeSpan EndTime);
}
