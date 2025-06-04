namespace WorkSchedulePlaner.Domain.Entities
{
	public class ScheduleUser
	{
		public int ScheduleId { get; set; }
		public WorkSchedule Schedule { get; set; }

		public string UserId { get; set; }

		public string Role { get; set; }
	}
}
