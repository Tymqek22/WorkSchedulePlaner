namespace WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs
{
	public class ShiftTileDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }
		public int ScheduleId { get; set; }
		public List<EmployeeWorkHoursDto>? Shifts { get; set; }
	}
}
