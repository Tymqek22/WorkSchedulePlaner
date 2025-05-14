namespace WorkSchedulePlaner.Application.Schedules.AddEmployee
{
	public record AddEmployeeRequest(
		string Name,
		string LastName,
		string? Position,
		int ScheduleId);
}