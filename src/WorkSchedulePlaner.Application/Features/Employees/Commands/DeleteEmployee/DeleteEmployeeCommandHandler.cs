using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Common.Results;
using WorkSchedulePlaner.Domain.Common.Errors;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee
{
	public class DeleteEmployeeCommandHandler
		: ICommandHandler<DeleteEmployeeCommand,Result>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteEmployeeCommandHandler(
			IRepository<Employee> employeeRepository,
			IWorkScheduleRepository workScheduleRepository,
			IUnitOfWork unitOfWork)
		{
			_workScheduleRepository = workScheduleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			DeleteEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(command.ScheduleId);

			if (schedule is null)
				return Result.Failure(Errors.Schedule.NotFound);

			var result = schedule.RemoveEmployee(command.EmployeeId);

			if (!result.IsSuccess)
				return result;

			await _unitOfWork.SaveAsync();

			return Result.Success();
		}
	}
}
