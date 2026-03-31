using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class Employee
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string LastName { get; private set; }
		public string? Position { get; private set; }

		public string? UserId { get; private set; }
		public int ScheduleId { get; private set; }
		public string ScheduleRole { get; private set; }

		private Employee() { }

		public Employee(
			string name,
			string lastName,
			int scheduleId,
			string scheduleRole,
			string? position,
			string? userId
			) 
		{
			Name = name;
			LastName = lastName;
			Position = position;
			UserId = userId;
			ScheduleId = scheduleId;
			ScheduleRole = scheduleRole;
		}
	}
}
