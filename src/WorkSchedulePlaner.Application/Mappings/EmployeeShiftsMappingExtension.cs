using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class EmployeeShiftsMappingExtension
	{
		public static EmployeeWorkHoursDto MapToDto(this EmployeeShift employeeShift)
		{
			return new EmployeeWorkHoursDto
			{
				Employee = employeeShift.Employee.MapToDto(),
				StartTime = employeeShift.StartTime,
				EndTime = employeeShift.EndTime
			};
		}
	}
}
