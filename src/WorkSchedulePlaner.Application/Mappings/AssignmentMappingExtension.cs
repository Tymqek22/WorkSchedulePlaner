using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Domain.ValueObjects;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class AssignmentMappingExtension
	{
		public static AssignmentDto MapToDto(this Assignment employeeShift)
		{
			return new AssignmentDto
			{
				EmployeeId = employeeShift.EmployeeId,
				DisplayName = employeeShift.DisplayName,
				StartTime = employeeShift.TimeRange.Start,
				EndTime = employeeShift.TimeRange.End
			};
		}
	}
}
