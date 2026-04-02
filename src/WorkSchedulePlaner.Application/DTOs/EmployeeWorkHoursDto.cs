namespace WorkSchedulePlaner.Application.DTOs
{
	public class EmployeeWorkHoursDto
	{
		public int EmployeeId { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
	}
}
