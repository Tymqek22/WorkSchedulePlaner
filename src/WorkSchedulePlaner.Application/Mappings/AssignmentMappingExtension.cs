using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class AssignmentMappingExtension
	{
		public static EmployeeWorkHoursDto MapToDto(this Assignment employeeShift)
		{
			return new EmployeeWorkHoursDto
			{
				EmployeeId = employeeShift.EmployeeId,
				StartTime = employeeShift.TimeRange.Start,
				EndTime = employeeShift.TimeRange.End
			};
		}
	}
}
