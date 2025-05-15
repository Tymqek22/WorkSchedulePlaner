using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee
{
	public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand,AddEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public AddEmployeeCommandHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
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
			await _employeeRepository.SaveAsync();

			return AddEmployeeResult.Success;
		}
	}
}
