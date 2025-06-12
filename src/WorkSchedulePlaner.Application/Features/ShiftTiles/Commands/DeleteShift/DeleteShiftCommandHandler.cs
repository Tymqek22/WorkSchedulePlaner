using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Errors;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift
{
	public class DeleteShiftCommandHandler : ICommandHandler<DeleteShiftCommand,Result>
	{
		private readonly IRepository<ShiftTile> _shiftTileRepository;
		private readonly IRepository<EmployeeShift> _shiftsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteShiftCommandHandler(
			IRepository<ShiftTile> shiftTileRepository,
			IRepository<EmployeeShift> shiftsRepository,
			IUnitOfWork unitOfWork)
		{
			_shiftTileRepository = shiftTileRepository;
			_shiftsRepository = shiftsRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			//find a shift tile
			var shiftTile = await _shiftTileRepository.GetByIdAsync(command.Id);

			if (shiftTile is null)
				return Result.Failure(Errors.ShiftTile.NotFound);

			//delete all shifts related with this tile
			await _shiftsRepository.DeleteManyAsync(es => es.ShiftTileId == shiftTile.Id);

			//delete shift tile
			await _shiftTileRepository.DeleteAsync(shiftTile.Id);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
