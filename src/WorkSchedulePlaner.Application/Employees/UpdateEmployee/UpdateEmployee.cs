using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.Schedules.AddEmployee;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Employees.UpdateEmployee
{
	public sealed class UpdateEmployee(IRepository<Employee> employeeRepository)
	{
		public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeRequest request)
		{
			var employee = await employeeRepository.GetByIdAsync(request.Id);

			if (employee is null)
				return UpdateEmployeeResult.Failure;

			employee.Name = request.Name;
			employee.LastName = request.LastName;
			employee.Position = request.Position;

			await employeeRepository.SaveAsync();

			return UpdateEmployeeResult.Success;
		}
	}
}
