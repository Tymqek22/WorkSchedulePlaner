using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleDetailsVM
	{
		public WorkSchedule Schedule { get; set; }
		public List<DateTime> Dates { get; set; }
	}
}
