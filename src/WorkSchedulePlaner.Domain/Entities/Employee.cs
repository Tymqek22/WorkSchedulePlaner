using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? Position { get; set; }

		public string? UserId { get; set; }

		public int ScheduleId { get; set; }
		public WorkSchedule Schedule { get; set; }

		public ICollection<EmployeeShift>? EmployeeShifts { get; set; }
	}
}
