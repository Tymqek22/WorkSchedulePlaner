using System.ComponentModel.DataAnnotations;

namespace WorkSchedulePlaner.Web.Requests
{
	public class AddScheduleRequest
	{
		[Required(ErrorMessage = "Schedule title is required")]
		public string Title { get; set; }
		public string OwnerId { get; set; }
	}
}
