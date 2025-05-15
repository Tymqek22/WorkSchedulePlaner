using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Schedules.AddEmployee
{
	public sealed class AddEmployee(IRepository<Employee> employeeRepository)
	{
		public async Task<AddEmployeeResult> Handle(AddEmployeeRequest request)
		{
			var employee = new Employee
			{
				Name = request.Name,
				LastName = request.LastName,
				Position = request.Position,
				ScheduleId = request.ScheduleId
			};

			await employeeRepository.InsertAsync(employee);
			await employeeRepository.SaveAsync();

			return AddEmployeeResult.Success;
		}
	}
}