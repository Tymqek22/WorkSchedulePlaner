using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
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

		public Result AddEmployee(string firstName,string lastName,int scheduleId,string? userId,string? position)
		{
			if (_employees.Any(e => e.FirstName == firstName && e.LastName == lastName))
				return Result.Failure(Errors.Schedule.EmployeeAlreadyExist);

			_employees.Add(new Employee(firstName,lastName,scheduleId,position,userId));

			return Result.Success();
		}

		public Result RemoveEmployee(int employeeId)
		{
			var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

			if (employee is null)
				return Result.Failure(Errors.Schedule.EmployeeNotFound);

			_employees.Remove(employee);

			return Result.Success();
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

			string employeeFullName = $"{employee.FirstName} {employee.LastName}";

			shiftTile.AssignEmployee(employeeId,employeeFullName,timeRange);
		}
	}
}
