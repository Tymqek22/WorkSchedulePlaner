namespace WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs
{
	public class EmployeeWorkHoursDto
	{
		public int EmployeeId { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
