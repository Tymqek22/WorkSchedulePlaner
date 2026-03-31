namespace WorkSchedulePlaner.Domain.ValueObjects
{
	public record Assignment(
			int EmployeeId,
			string DisplayName,
			TimeRange TimeRange
		);
}
