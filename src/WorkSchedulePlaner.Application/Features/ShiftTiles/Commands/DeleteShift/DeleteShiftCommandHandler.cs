using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift
{
	public class DeleteShiftCommandHandler : ICommandHandler<DeleteShiftCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteShiftCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteShiftCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(command.scheduleId);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var result = schedule.DeleteShift(command.shiftId);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
