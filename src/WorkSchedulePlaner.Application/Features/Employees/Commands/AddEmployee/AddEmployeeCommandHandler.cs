using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Abstractions.Services;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand,AddEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<ScheduleUser> _scheduleUserRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IIdentityService _identityService;

		public AddEmployeeCommandHandler(
			IRepository<Employee> employeeRepository,
			IRepository<ScheduleUser> scheduleUserRepository,
			IUnitOfWork unitOfWork,
			IIdentityService identityService)
		{
			_employeeRepository = employeeRepository;
			_scheduleUserRepository = scheduleUserRepository;
			_unitOfWork = unitOfWork;
			_identityService = identityService;
		}

		public async Task<AddEmployeeResult> Handle(
			AddEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			string userId = null;

			if (command.UserEmail is not null) {

				userId = await _identityService.GetUserIdByEmail(command.UserEmail);

				if (userId is null)
					return AddEmployeeResult.Failure;

				var scheduleUser = new ScheduleUser
				{
					ScheduleId = command.ScheduleId,
					UserId = userId,
					Role = "employee"
				};

				await _scheduleUserRepository.InsertAsync(scheduleUser);
			}

			var employee = new Employee
			{
				Name = command.Name,
				LastName = command.LastName,
				Position = command.Position,
				UserId = userId,
				ScheduleId = command.ScheduleId
			};

			await _employeeRepository.InsertAsync(employee);
			await _unitOfWork.SaveAsync();

			return AddEmployeeResult.Success;
		}
	}
}
