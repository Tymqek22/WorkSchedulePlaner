namespace WorkSchedulePlaner.Application.DTOs
{
	public class ShiftTileDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateOnly Date { get; set; }
		public List<AssignmentDto>? Shifts { get; set; }
	}
}
