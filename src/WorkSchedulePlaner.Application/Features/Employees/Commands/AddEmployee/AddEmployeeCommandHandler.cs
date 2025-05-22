using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand,AddEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AddEmployeeCommandHandler(IRepository<Employee> employeeRepository,IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<AddEmployeeResult> Handle(
			AddEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			var employee = new Employee
			{
				Name = command.Name,
				LastName = command.LastName,
				Position = command.Position,
				ScheduleId = command.ScheduleId
			};

			await _employeeRepository.InsertAsync(employee);
			await _unitOfWork.SaveAsync();

			return AddEmployeeResult.Success;
		}
	}
}
