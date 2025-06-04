using System.ComponentModel.DataAnnotations;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class WorkSchedule
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string OwnerId { get; set; }

		public ICollection<Employee> Employees { get; set; }
		public ICollection<ShiftTile> ShiftTiles { get; set; }
		public ICollection<ScheduleUser> UsersInSchedule { get; set; }
	}
}
