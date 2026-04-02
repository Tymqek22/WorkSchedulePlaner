using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Application.Mappings
{
	public static class ShiftTileMappingExtension
	{
		public static ShiftTileDto MapToDto(this ShiftTile shiftTile)
		{
			return new ShiftTileDto
			{
				Id = shiftTile.Id,
				Title = shiftTile.Title,
				Description = shiftTile.Description,
				Date = shiftTile.Date,
				Shifts = shiftTile.Assignments
					.Select(a => a.MapToDto())
					.ToList()
			};
		}
	}
}
