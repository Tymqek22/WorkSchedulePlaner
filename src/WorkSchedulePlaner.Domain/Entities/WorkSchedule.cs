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

		public Result AddEmployee(
			string firstName,
			string lastName,
			int scheduleId,
			string? position,
			string? email,
			string? userId)
		{
			if (_employees.Any(e => e.FirstName == firstName && e.LastName == lastName))
				return Result.Failure(Errors.Schedule.EmployeeAlreadyExist);

			var employeeToAdd = new Employee(firstName,lastName,scheduleId,position);
			employeeToAdd.LinkEmployeeWithUser(email,userId);

			_employees.Add(employeeToAdd);

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

		public Result UpdateEmployee(
			int employeeId, 
			string firstName, 
			string lastName, 
			string? position,
			string? email,
			string? userId)
		{
			var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

			if (employee is null)
				return Result.Failure(Errors.Schedule.EmployeeNotFound);

			var updateResult = employee.UpdateDetails(firstName,lastName,position);
			employee.LinkEmployeeWithUser(email,userId);

			if (!updateResult.IsSuccess)
				return updateResult;

			return Result.Success();
		}

		public async Task<Result> CreateShift(
			string title,
			string? description,
			List<ShiftAssignment> shiftAssignments)
		{
			var newShift = new ShiftTile(title,Id,description);

			foreach (var assignment in shiftAssignments) {

				var employee = _employees.FirstOrDefault(e => e.Id == assignment.EmployeeId);

				if (employee is null)
					return Result.Failure(Errors.Schedule.EmployeeNotFound);

				string employeeFullName = $"{employee.FirstName} {employee.LastName}";

				var result = newShift.AssignEmployee(assignment.EmployeeId,employeeFullName,assignment.TimeRange);

				if (!result.IsSuccess)
					return result;
			}

			_shiftTiles.Add(newShift);
			return Result.Success();
		}
	}
}
