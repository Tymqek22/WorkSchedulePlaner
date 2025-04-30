using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class ShiftTile
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }

		[ForeignKey("ScheduleId")]
		public int ScheduleId { get; set; }
		public WorkSchedule Schedule { get; set; }

		public List<EmployeeShift> EmployeeShifts { get; set; }
	}
}
