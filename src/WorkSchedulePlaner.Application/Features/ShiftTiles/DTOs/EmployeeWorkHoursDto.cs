using WorkSchedulePlaner.Application.Features.Employees.DTOs;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs
{
	public class EmployeeWorkHoursDto
	{
		public EmployeeDto Employee { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
