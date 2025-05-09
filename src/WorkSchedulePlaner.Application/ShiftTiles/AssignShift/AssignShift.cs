using WorkSchedulePlaner.Application.DTO;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.ShiftTiles.AssignShift
{
	public sealed class AssignShift(
		IRepository<Employee> employeeRepository,
		IRepository<ShiftTile> shiftTileRepository,
		IRepository<EmployeeShift> employeeShiftRepository)
	{
		private async Task<bool> AllEmployeesExist(List<EmployeeWorkHoursDto> employeeWorkHours)
		{
			foreach (var employee in employeeWorkHours) {

				var exists = await employeeRepository.GetByIdAsync(employee.EmployeeId);

				if (exists is null)
					return false;
			}

			return true;
		}

		public async Task<AssignShiftResult> Handle(AssignShiftRequest request)
		{
			if (!await this.AllEmployeesExist(request.EmployeeWorkHours))
				return AssignShiftResult.UserNotFound;

			//add tile to DB
			var newShiftTile = new ShiftTile
			{
				Title = request.Title,
				Description = request.Description,
				Date = request.Date,
				ScheduleId = request.ScheduleId
			};

			await shiftTileRepository.InsertAsync(newShiftTile);
			await shiftTileRepository.SaveAsync();

			//add EmployeeShift to DB
			foreach (var employeeShift in request.EmployeeWorkHours) {

				var newEmployeeShift = new EmployeeShift
				{
					EmployeeId = employeeShift.EmployeeId,
					ShiftTileId = newShiftTile.Id,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				};

				await employeeShiftRepository.InsertAsync(newEmployeeShift);
			}
			await employeeShiftRepository.SaveAsync();

			return AssignShiftResult.Success;
		}
	}
}
