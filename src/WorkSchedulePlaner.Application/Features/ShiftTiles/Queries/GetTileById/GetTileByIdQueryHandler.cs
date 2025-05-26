using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById
{
	public class GetTileByIdQueryHandler : IQueryHandler<GetTileByIdQuery,ShiftTileDto>
	{
		private readonly IShiftTileRepository _shiftTileRepository;

		public GetTileByIdQueryHandler(IShiftTileRepository shiftTileRepository)
		{
			_shiftTileRepository = shiftTileRepository;
		}

		public async Task<ShiftTileDto> Handle(
			GetTileByIdQuery query,
			CancellationToken cancellationToken = default)
		{
			var tile = await _shiftTileRepository.GetByIdWithAllIncludes(query.Id);

			return tile.MapToDto();
		}
	}
}
