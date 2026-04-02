namespace WorkSchedulePlaner.Domain.ValueObjects
{
	public class TimeRange
	{
		public TimeOnly Start { get; init; }
		public TimeOnly End { get; init; }

		private TimeRange() { }

		public TimeRange(TimeOnly start,TimeOnly end)
		{
			if (start >= end)
				throw new ArgumentException("End time must be after start time.");

			Start = start;
			End = end;
		}
	}
}
