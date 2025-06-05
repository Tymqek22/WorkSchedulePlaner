using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ScheduleDetailsVM
	{
		public WorkScheduleDto Schedule { get; set; }
		public List<DateTime> Dates { get; set; }
	}
}
