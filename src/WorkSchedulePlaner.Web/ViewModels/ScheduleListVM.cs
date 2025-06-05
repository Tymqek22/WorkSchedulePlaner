using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleListVM
	{
		public List<WorkScheduleDto> Schedules { get; set; }
		public bool IsCurrentUserAdmin { get; set; }
	}
}
