using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class WorkSchedule
	{
		private readonly List<Employee> _employees = new();
		private readonly List<ShiftTile> _shiftTiles = new();

		public int Id { get; private set; }
		public string Title { get; private set; }
		public string OwnerId { get; private set; }

		public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();
		public IReadOnlyCollection<ShiftTile> ShiftTiles => _shiftTiles.AsReadOnly();

		private WorkSchedule() { }

		public WorkSchedule(string title, string ownerId)
		{
			Title = title;
			OwnerId = ownerId;
		}
	}
}
