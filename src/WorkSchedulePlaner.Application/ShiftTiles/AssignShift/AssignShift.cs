using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.ShiftTiles.AssignShift
{
	public sealed class AssignShift(
		IRepository<Employee> employeeRepository,
		IRepository<ShiftTile> shiftTileRepository,
		IRepository<EmployeeShift> employeeShiftRepository)
	{
		public async Task<AssignShiftResult> Handle(AssignShiftRequest request)
		{
			//find user in DB
			var employee = await employeeRepository.GetByIdAsync(request.UserId);
			
			if (employee is null) {

				return AssignShiftResult.UserNotFound;
			}

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
			var employeeShift = new EmployeeShift
			{
				EmployeeId = request.UserId,
				ShiftTileId = newShiftTile.Id,
				StartTime = request.StartTime,
				EndTime = request.EndTime
			};

			await employeeShiftRepository.InsertAsync(employeeShift);
			await employeeShiftRepository.SaveAsync();

			return AssignShiftResult.Success;
		}
	}
}
