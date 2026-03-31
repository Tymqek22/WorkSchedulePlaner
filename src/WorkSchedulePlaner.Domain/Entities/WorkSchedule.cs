using WorkSchedulePlaner.Domain.ValueObjects;

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

		public void AddEmployee(string name,string lastName,int scheduleId,string? userId,string? position)
		{
			if (_employees.Any(e => e.Name == name && e.LastName == lastName))
				throw new ArgumentException("Employee is already assigned to schedule.");

			_employees.Add(new Employee(name,lastName,scheduleId,position,userId));
		}

		public void RemoveEmployee(int employeeId)
		{
			var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

			if (employee is null)
				throw new ArgumentException($"Employee with id:{employeeId} is not in a schedule.");

			_employees.Remove(employee);
		}

		public void AddShiftTile(string title, string? description)
		{
			_shiftTiles.Add(new ShiftTile(title,Id,description));
		}

		public void AssignShift(int shiftTileId, int employeeId, TimeRange timeRange)
		{
			var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

			if (employee is null)
				throw new ArgumentException($"Employee with that id:{employeeId} is not in schedule.");

			var shiftTile = _shiftTiles.FirstOrDefault(st => st.Id == shiftTileId);

			if (shiftTile is null)
				throw new ArgumentException($"Shift tile with that id:{shiftTileId} doesn't exist.");

			string employeeFullName = $"{employee.Name} {employee.LastName}";

			shiftTile.AssignEmployee(employeeId,employeeFullName,timeRange);
		}
	}
}
