using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ShiftAssignmentVM
	{
		public ShiftTile ShiftTile { get; set; }
		public int EmployeeId { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
