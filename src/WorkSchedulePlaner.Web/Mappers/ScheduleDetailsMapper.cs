using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Web.ViewModels;

namespace WorkSchedulePlaner.Web.Mappers
{
	public static class ScheduleDetailsMapper
	{
		public static ScheduleDetailsVM MapToVM(
			WorkScheduleDto schedule,
			List<DateTime> dates,
			bool isAdmin,
			int weekOffset)
		{
			var viewModel = new ScheduleDetailsVM
			{
				Schedule = new WorkScheduleVM
				{
					ScheduleId = schedule.Id,
					Title = schedule.Title,
					IsAdmin = schedule.IsAdmin
				},
				Shifts = schedule.ShiftTiles
					.Select(st => new ShiftTileVM
					{
						ShiftTileId = st.Id,
						Title = st.Title,
						Description = st.Description,
						Date = st.Date,
						Assignments = st.Shifts
							.Select(s => new AssignmentVM
							{
								DisplayName = s.DisplayName,
								StartTime = s.StartTime,
								EndTime = s.EndTime,
							})
							.ToList()
					})
					.ToList(),
				Dates = dates.Select(d => DateOnly.FromDateTime(d)).ToList(),
				IsCurrentUserAdmin = isAdmin,
				WeekOffset = weekOffset
			};

			return viewModel;
		}
	}
}
