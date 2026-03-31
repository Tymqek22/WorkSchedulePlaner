namespace WorkSchedulePlaner.Domain.ValueObjects
{
	public record Assignment(
			string DisplayName,
			TimeOnly StartTime,
			TimeOnly EndTime
		);
}
