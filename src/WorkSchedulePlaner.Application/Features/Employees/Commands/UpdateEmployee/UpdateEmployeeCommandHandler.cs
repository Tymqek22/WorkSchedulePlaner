using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IScheduleUserRepository _scheduleUserRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IIdentityService _identityService;

		public UpdateEmployeeCommandHandler(
			IRepository<Employee> employeeRepository,
			IScheduleUserRepository scheduleUserRepository,
			IUnitOfWork unitOfWork,
			IIdentityService identityService)
		{
			_employeeRepository = employeeRepository;
			_scheduleUserRepository = scheduleUserRepository;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<UpdateEmployeeResult> Handle(
			UpdateEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			var employee = await _employeeRepository.GetByIdAsync(command.Id);

			if (employee is null)
				return UpdateEmployeeResult.Failure;

			if (command.Email is not null) {

				var userId = await _identityService.GetUserIdByEmail(command.Email);

				if (userId is null)
					return UpdateEmployeeResult.Failure;

				if (employee.UserId is not null) 
					await _scheduleUserRepository.DeleteAsyncByIds(employee.UserId,employee.ScheduleId);

				var scheduleUser = new ScheduleUser
				{
					UserId = userId,
					ScheduleId = employee.ScheduleId,
					Role = "employee"
				};

				await _scheduleUserRepository.InsertAsync(scheduleUser);
				employee.UserId = userId;
			}
			else {
				await _scheduleUserRepository.DeleteAsyncByIds(employee.UserId,employee.ScheduleId);
				employee.UserId = null;
			}

			employee.Name = command.Name;
			employee.LastName = command.LastName;
			employee.Position = command.Position;

			await _employeeRepository.UpdateAsync(employee);
			await _unitOfWork.SaveAsync();

			return UpdateEmployeeResult.Success;
		}
	}
}
