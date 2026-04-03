using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleDetailsVM
	{
		public WorkScheduleVM Schedule { get; set; }
		public List<ShiftTileVM> Shifts { get; set; }
		public List<DateOnly> Dates { get; set; }
		public bool IsCurrentUserAdmin { get; set; }
		public int WeekOffset { get; set; }
	}
}
