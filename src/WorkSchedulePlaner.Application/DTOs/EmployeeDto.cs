namespace WorkSchedulePlaner.Application.DTOs
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? Position { get; set; }
		public string? Email { get; set; }
		public int ScheduleId { get; set; }
	}
}
