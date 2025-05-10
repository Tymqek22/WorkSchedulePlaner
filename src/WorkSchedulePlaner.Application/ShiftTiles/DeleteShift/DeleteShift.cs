using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.ShiftTiles.DeleteShift
{
	public sealed class DeleteShift(
		IRepository<ShiftTile> shiftTileRepository,
		IEmployeeShiftRepository shiftsRepository)
	{
		public async Task<DeleteShiftResult> Handle(DeleteShiftRequest request)
		{
			//find a shift tile
			var shiftTile = await shiftTileRepository.GetByIdAsync(request.Id);

			if (shiftTile is null)
				return DeleteShiftResult.Failure;

			//delete all shifts related with this tile
			await shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == shiftTile.Id);
			await shiftsRepository.SaveAsync();

			//delete shift tile
			await shiftTileRepository.DeleteAsync(shiftTile.Id);
			await shiftTileRepository.SaveAsync();

			return DeleteShiftResult.Success;
		}
	}
}
