namespace WorkSchedulePlaner.Application.DTOs
{
	public class EmployeeWorkHoursDto
	{
		public EmployeeDto Employee { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
