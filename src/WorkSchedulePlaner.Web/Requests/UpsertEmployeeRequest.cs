using System.ComponentModel.DataAnnotations;

namespace WorkSchedulePlaner.Web.Requests
{
	public class UpsertEmployeeRequest
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "First name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; }
		public string? Position { get; set; }
		public string? Email { get; set; }
		public int ScheduleId { get; set; }
	}
}
