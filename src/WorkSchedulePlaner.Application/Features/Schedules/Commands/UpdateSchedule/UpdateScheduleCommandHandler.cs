using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule
{
	public class UpdateScheduleCommandHandler
		: ICommandHandler<UpdateScheduleCommand,UpdateScheduleResult>
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

		public async Task<UpdateScheduleResult> Handle(
			UpdateScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _scheduleRepository.GetByIdAsync(command.Id);

			if (schedule is null)
				return UpdateScheduleResult.Failure;

			schedule.Title = command.Title;

			await _scheduleRepository.UpdateAsync(schedule);
			await _unitOfWork.SaveAsync();

			return UpdateScheduleResult.Success;
		}
	}
}
