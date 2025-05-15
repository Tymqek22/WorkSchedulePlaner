using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public class UpdateEmployeeComandHandler : ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;

		public UpdateEmployeeComandHandler(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<UpdateEmployeeResult> Handle(
			UpdateEmployeeCommand command,
			CancellationToken cancellationToken = default)
		{
			var employee = await _employeeRepository.GetByIdAsync(command.Id);

			if (employee is null)
				return UpdateEmployeeResult.Failure;

			employee.Name = command.Name;
			employee.LastName = command.LastName;
			employee.Position = command.Position;

			await _employeeRepository.SaveAsync();

			return UpdateEmployeeResult.Success;
		}
	}
}
