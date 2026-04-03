using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ShiftTileVM
	{
		public int ShiftTileId { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateOnly Date { get; set; }
		public List<AssignmentVM> Assignments { get; set; }
	}
}