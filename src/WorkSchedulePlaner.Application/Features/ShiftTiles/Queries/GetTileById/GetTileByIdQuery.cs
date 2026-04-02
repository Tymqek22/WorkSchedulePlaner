using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById
{
	public record GetTileByIdQuery(int scheduleId,int shiftId)
		: IQuery<ShiftTileDto>;
}
