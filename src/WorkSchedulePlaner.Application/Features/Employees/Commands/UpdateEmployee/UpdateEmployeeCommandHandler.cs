using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee
{
	public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult>
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateEmployeeCommandHandler(IRepository<Employee> employeeRepository,IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_unitOfWork = unitOfWork;
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

			await _employeeRepository.UpdateAsync(employee);
			await _unitOfWork.SaveAsync();

			return UpdateEmployeeResult.Success;
		}
	}
}
