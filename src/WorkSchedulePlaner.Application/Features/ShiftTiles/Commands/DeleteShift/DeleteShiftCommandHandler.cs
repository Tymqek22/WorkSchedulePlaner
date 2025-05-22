using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift
{
	public class DeleteShiftCommandHandler : ICommandHandler<DeleteShiftCommand,DeleteShiftResult>
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IEmployeeShiftRepository _shiftsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteShiftCommandHandler(
			IRepository<ShiftTile> shiftTileRepository,
			IEmployeeShiftRepository shiftsRepository,
			IUnitOfWork unitOfWork)
		{
			_shiftTileRepository = shiftTileRepository;
			_shiftsRepository = shiftsRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<DeleteShiftResult> Handle(
			DeleteShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			//find a shift tile
			var shiftTile = await _shiftTileRepository.GetByIdAsync(command.Id);

			if (shiftTile is null)
				return DeleteShiftResult.Failure;

			//delete all shifts related with this tile
			await _shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == shiftTile.Id);

			//delete shift tile
			await _shiftTileRepository.DeleteAsync(shiftTile.Id);
			await _unitOfWork.SaveAsync();

			return DeleteShiftResult.Success;
		}
	}
}
