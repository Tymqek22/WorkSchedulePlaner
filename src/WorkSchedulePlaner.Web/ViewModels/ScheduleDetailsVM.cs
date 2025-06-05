using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleDetailsVM
	{
		public WorkScheduleDto Schedule { get; set; }
		public List<DateTime> Dates { get; set; }
		public bool IsCurrentUserAdmin { get; set; }
	}
}
