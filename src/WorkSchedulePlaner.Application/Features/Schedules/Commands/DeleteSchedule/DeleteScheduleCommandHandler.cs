using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule
{
	public class DeleteScheduleCommandHandler
		: ICommandHandler<DeleteScheduleCommand,Result>
	{
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteScheduleCommandHandler(
			IWorkScheduleRepository scheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_scheduleRepository = scheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _scheduleRepository.GetByIdAsync(command.Id);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			await _scheduleRepository.DeleteAsync(schedule.Id);
			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
