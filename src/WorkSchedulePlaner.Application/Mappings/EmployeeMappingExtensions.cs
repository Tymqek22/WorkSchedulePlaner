using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class EmployeeMappingExtensions
	{
		public static EmployeeDto MapToDto(this Employee employee)
		{
			return new EmployeeDto
			{
				Id = employee.Id,
				Name = employee.Name,
				LastName = employee.LastName,
				Position = employee.Position,
				ScheduleId = employee.ScheduleId
			};
		}
	}
}
