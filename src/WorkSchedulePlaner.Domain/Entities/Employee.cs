using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? Position { get; set; }

		[ForeignKey("ScheduleId")]
		public int ScheduleId { get; set; }
		public WorkSchedule Schedule { get; set; }

		public List<EmployeeShift>? EmployeeShifts { get; set; }
	}
}
