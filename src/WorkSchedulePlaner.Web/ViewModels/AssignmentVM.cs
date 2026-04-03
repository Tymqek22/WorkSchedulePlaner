namespace WorkSchedulePlaner.Web.ViewModels
{
	public class AssignmentVM
	{
		public string DisplayName { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
	}
}