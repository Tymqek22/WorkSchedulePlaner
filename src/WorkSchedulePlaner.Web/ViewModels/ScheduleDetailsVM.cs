using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleDetailsVM
	{
		public WorkScheduleDto Schedule { get; set; }
		public List<DateOnly> Dates { get; set; }
		public bool IsCurrentUserAdmin { get; set; }
	}
}
