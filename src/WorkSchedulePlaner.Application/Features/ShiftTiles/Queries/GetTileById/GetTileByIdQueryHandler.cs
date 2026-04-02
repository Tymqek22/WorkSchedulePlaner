using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Mappings;
using WorkSchedulePlaner.Domain.Repositories;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById
{
	public class GetTileByIdQueryHandler : IQueryHandler<GetTileByIdQuery,ShiftTileDto>
	{
		private readonly IWorkScheduleRepository _workScheduleRepository;

		public GetTileByIdQueryHandler(IWorkScheduleRepository workScheduleRepository)
		{
			_workScheduleRepository = workScheduleRepository;
		}

		public async Task<ShiftTileDto> Handle(
			GetTileByIdQuery query,
			CancellationToken cancellationToken = default)
		{
			var schedule = await _workScheduleRepository.GetByIdWithDetailsAsync(query.scheduleId);

			var tile = schedule.ShiftTiles.FirstOrDefault(st => st.Id == query.shiftId);

			return tile.MapToDto();
		}
	}
}
