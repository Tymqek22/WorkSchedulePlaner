namespace WorkSchedulePlaner.Domain.Entities
{
	public class EmployeeShift
	{
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }

		public int ShiftTileId { get; set; }
		public ShiftTile ShiftTile { get; set; }

		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
