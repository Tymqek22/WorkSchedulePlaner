using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift
{
	public class UpdateShiftCommandHandler : ICommandHandler<UpdateShiftCommand,UpdateShiftResult>
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IEmployeeShiftRepository _shiftsRepository;

		public UpdateShiftCommandHandler(
			IRepository<ShiftTile> shiftTileRepository,
			IEmployeeShiftRepository shiftsRepository)
		{
			_shiftTileRepository = shiftTileRepository;
			_shiftsRepository = shiftsRepository;
		}

		public async Task<UpdateShiftResult> Handle(
			UpdateShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			//find tile in db
			var tile = await _shiftTileRepository.GetByIdAsync(command.ShiftTileId);

			if (tile is null)
				return UpdateShiftResult.Failure;

			//delete employee shifts for that tile
			await _shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == command.ShiftTileId);
			await _shiftsRepository.SaveAsync();

			//add updated shifts for tile
			foreach (var employeeShift in command.EmployeeWorkHours) {

				var shift = new EmployeeShift
				{
					EmployeeId = employeeShift.EmployeeId,
					ShiftTileId = command.ShiftTileId,
					StartTime = employeeShift.StartTime,
					EndTime = employeeShift.EndTime
				};

				await _shiftsRepository.InsertAsync(shift);
			}
			await _shiftsRepository.SaveAsync();

			//update rest of tile
			tile.Title = command.Title;
			tile.Description = command.Description;

			await _shiftTileRepository.SaveAsync();

			return UpdateShiftResult.Success;
		}
	}
}
