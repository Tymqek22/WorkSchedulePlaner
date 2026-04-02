using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IIdentityService _identityService;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateEmployeeCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork,
			IIdentityService identityService)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<Result> Handle(
			UpdateEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			string? userId = null;

			if (command.Email is not null) {

				userId = await _identityService.GetUserIdByEmail(command.Email);

				if (userId is null)
					return Result.Failure(Errors.Employee.NotFound);
			}

			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(command.ScheduleId);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var result = schedule.UpdateEmployee(
				command.Id,
				command.FirstName,
				command.LastName,
				command.Position,
				command.Email,
				userId);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
