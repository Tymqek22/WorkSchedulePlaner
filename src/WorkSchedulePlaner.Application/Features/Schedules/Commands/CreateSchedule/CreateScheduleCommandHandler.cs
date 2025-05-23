using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule
{
	public class CreateScheduleCommandHandler
		: ICommandHandler<CreateScheduleCommand,CreateScheduleResult>
	{
		private readonly IWorkScheduleRepository _scheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateScheduleCommandHandler(
			IWorkScheduleRepository scheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_scheduleRepository = scheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<CreateScheduleResult> Handle(
			CreateScheduleCommand command,
			CancellationToken cancellationToken = default)
		{
			var newSchedule = new WorkSchedule
			{
				Title = command.Title
			};

			await _scheduleRepository.InsertAsync(newSchedule);
			await _unitOfWork.SaveAsync();

			return CreateScheduleResult.Success;
		}
	}
}
