using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule
{
	public class UpdateScheduleCommandHandler
		: ICommandHandler<UpdateScheduleCommand,Result>
	{
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateScheduleCommandHandler(
			IWorkScheduleRepository scheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_scheduleRepository = scheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			UpdateScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _scheduleRepository.GetByIdAsync(command.Id);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var result = schedule.UpdateTitle(command.Title);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
