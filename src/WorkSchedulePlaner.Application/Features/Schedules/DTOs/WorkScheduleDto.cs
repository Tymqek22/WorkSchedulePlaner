using WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs;

namespace WorkSchedulePlaner.Application.Features.Schedules.DTOs
{
	public class WorkScheduleDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<ShiftTileDto>? ShiftTiles { get; set; }
	}
}
