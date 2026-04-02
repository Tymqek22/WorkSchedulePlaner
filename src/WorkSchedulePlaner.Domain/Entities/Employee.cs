using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class Employee
	{
		public int Id { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string? Position { get; private set; }
		public string? Email { get; private set; }

		public string? UserId { get; private set; }
		public int ScheduleId { get; private set; }

		private Employee() { }

		public Employee(
				string firstName,
				string lastName,
				int scheduleId,
				string? position
			) 
		{
			FirstName = firstName;
			LastName = lastName;
			Position = position;
			ScheduleId = scheduleId;
		}

		public Result UpdateDetails(string firstName, string lastName, string? position)
		{
			if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
				return Result.Failure(Errors.Employee.ArgumentsInWrongFormat);

			FirstName = firstName;
			LastName = lastName;
			Position = position;

			return Result.Success();
		}

		public void LinkEmployeeWithUser(string email, string userId)
		{
			Email = email;
			UserId = userId;
		}
	}
}
