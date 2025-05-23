namespace WorkSchedulePlaner.Application.Features.Employees.DTOs
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? Position { get; set; }
		public int ScheduleId { get; set; }
	}
}
