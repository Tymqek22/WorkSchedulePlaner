namespace WorkSchedulePlaner.Application.DTOs
{
	public class AssignmentDto
	{
		public int EmployeeId { get; set; }
		public string DisplayName { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
	}
}
