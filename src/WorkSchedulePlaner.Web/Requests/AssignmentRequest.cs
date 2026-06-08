namespace WorkSchedulePlaner.Web.Requests
{
	public class AssignmentRequest
	{
		public int EmployeeId { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
	}
}
