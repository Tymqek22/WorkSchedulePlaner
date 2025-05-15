using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift
{
	public record DeleteShiftCommand(int Id)
		: ICommand<DeleteShiftResult>;
}
