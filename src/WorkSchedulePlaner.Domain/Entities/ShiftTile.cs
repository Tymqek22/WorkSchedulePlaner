using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class ShiftTile
	{
		private readonly List<Assignment> _assignments = new();

		public int Id { get; private set; }
		public string Title { get; private set; }
		public string? Description { get; private set; }
		public DateOnly Date { get; private set; }

		public int ScheduleId { get; private set; }

		public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

		private ShiftTile() { }

		public ShiftTile(string title, string? description)
		{
			Title = title;
			Description = description;
		}
	}
}
