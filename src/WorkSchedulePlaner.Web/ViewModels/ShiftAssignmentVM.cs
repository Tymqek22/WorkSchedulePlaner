using WorkSchedulePlaner.Application.Features.ShiftTiles.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Web.ViewModels
{
	public class ShiftAssignmentVM
	{
		public ShiftTile ShiftTile { get; set; }
		public List<EmployeeWorkHoursDto> EmployeeWorkHours { get; set; }
	}
}
