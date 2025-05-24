using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class WorkScheduleMappingExtension
	{
		public static WorkScheduleDto MapToDto(this WorkSchedule workSchedule)
		{
			return new WorkScheduleDto
			{
				Id = workSchedule.Id,
				Title = workSchedule.Title,
				ShiftTiles = workSchedule.ShiftTiles
					.Select(st => st.MapToDto())
					.ToList()
			};
		}
	}
}
