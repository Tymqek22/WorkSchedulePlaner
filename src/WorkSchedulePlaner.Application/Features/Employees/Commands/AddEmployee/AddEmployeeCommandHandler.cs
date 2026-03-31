using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IIdentityService _identityService;

		public AddEmployeeCommandHandler(
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork,
			IIdentityService identityService)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<Result> Handle(
			AddEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			string? userId = null;

			if (command.UserEmail is not null) {

				userId = await _identityService.GetUserIdByEmail(command.UserEmail);

				if (userId is null)
					return Result.Failure(Errors.Employee.NotFound);
			}

			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(command.ScheduleId);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var result = schedule.AddEmployee(
				command.FirstName,
				command.LastName,
				schedule.Id,
				userId,
				command.Position);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
