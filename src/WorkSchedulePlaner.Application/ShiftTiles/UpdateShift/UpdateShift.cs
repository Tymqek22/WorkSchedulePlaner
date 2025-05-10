using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.ShiftTiles.UpdateShift
{
	public sealed class UpdateShift(
		IRepository<ShiftTile> shiftTileRepository,
		IEmployeeShiftRepository shiftsRepository)
	{
		public async Task<UpdateShiftResult> Handle(UpdateShiftRequest request)
		{
			//find tile in db
			var tile = await shiftTileRepository.GetByIdAsync(request.ShiftTileId);

			if (tile is null)
				return UpdateShiftResult.Failure;

			//delete employee shifts for that tile
			await shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == request.ShiftTileId);
			
			//add updated shifts for tile
			foreach (var employeeShift in request.EmployeeWorkHours) {

				var shift = new EmployeeShift
				{
					EmployeeId = employeeShift.EmployeeId,
					ShiftTileId = request.ShiftTileId,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				};

				await shiftsRepository.InsertAsync(shift);
			}
			await shiftsRepository.SaveAsync();

			//update rest of tile
			tile.Title = request.Title;
			tile.Description = request.Description;

			await shiftTileRepository.SaveAsync();

			return UpdateShiftResult.Success;
		}
	}
}
