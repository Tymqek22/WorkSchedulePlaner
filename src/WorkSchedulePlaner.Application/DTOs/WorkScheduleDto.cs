namespace WorkSchedulePlaner.Application.DTOs
{
	public class WorkScheduleDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<ShiftTileDto>? ShiftTiles { get; set; }
	}
}
