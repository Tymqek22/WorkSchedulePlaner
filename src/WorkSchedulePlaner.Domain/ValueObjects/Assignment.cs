namespace WorkSchedulePlaner.Domain.ValueObjects
{
	public class Assignment
	{
		public int EmployeeId { get; init; }
		public string DisplayName { get; init; }
		public TimeRange TimeRange { get; init; }

		private Assignment() { }

		public Assignment(int employeeId, string displayName, TimeRange timeRange)
		{
			EmployeeId = employeeId;
			DisplayName = displayName;
			TimeRange = timeRange;
		}
	}
}
