using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
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

		public ShiftTile(string title, int scheduleId, string? description)
		{
			Title = title;
			Description = description;
			ScheduleId = scheduleId;
			Date = DateOnly.FromDateTime(DateTime.UtcNow);
		}

		public Result AssignEmployee(int employeeId, string displayName, TimeRange timeRange)
		{
			if (_assignments.Any(a => a.EmployeeId == employeeId))
				return Result.Failure(Errors.ShiftTile.TooManyEmployeeAssignments);

			_assignments.Add(new Assignment(employeeId,displayName,timeRange));

			return Result.Success();
		}
	}
}
