using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Mappers
{
	public static class ShiftTileMapper
	{
		public static ShiftTileVM MapToVM(
			ShiftTileDto shiftTile,
			int scheduleId)
		{
			return new ShiftTileVM
			{
				ShiftTileId = shiftTile.Id,
				Title = shiftTile.Title,
				Description = shiftTile.Description,
				Date = shiftTile.Date,
				Assignments = shiftTile.Shifts
					.Select(s => new AssignmentVM
					{
						DisplayName = s.DisplayName,
						StartTime = s.StartTime,
						EndTime = s.EndTime
					})
					.ToList(),
				ScheduleId = scheduleId
			};
		}
	}
}
