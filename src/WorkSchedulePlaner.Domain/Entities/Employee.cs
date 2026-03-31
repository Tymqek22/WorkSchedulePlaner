namespace WorkSchedulePlaner.Domain.Entities
{
	public class Employee
	{
		public int Id { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string? Position { get; private set; }

		public string? UserId { get; private set; }
		public int ScheduleId { get; private set; }

		private Employee() { }

		public Employee(
				string firstName,
				string lastName,
				int scheduleId,
				string? position,
				string? userId
			) 
		{
			FirstName = firstName;
			LastName = lastName;
			Position = position;
			UserId = userId;
			ScheduleId = scheduleId;
		}
	}
}
