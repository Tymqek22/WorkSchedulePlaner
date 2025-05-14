namespace WorkSchedulePlaner.Application.Schedules.AddEmployee
{
	public record UpdateEmployeeRequest(
		int Id,
		string Name,
		string LastName,
		string? Position);
}