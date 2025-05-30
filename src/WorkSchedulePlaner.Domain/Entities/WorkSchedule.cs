using System.ComponentModel.DataAnnotations;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class WorkSchedule
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string OwnerId { get; set; }

		public List<Employee> Employees { get; set; }
		public List<ShiftTile> ShiftTiles { get; set; }
	}
}
