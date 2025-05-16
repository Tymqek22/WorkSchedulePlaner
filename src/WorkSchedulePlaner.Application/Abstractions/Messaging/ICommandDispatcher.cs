namespace WorkSchedulePlaner.Application.Abstractions.Messaging
{
	public interface ICommandDispatcher
	{
		Task Dispatch<TCommand>(TCommand command) 
			where TCommand : ICommand;
		Task<TResponse> Dispatch<TCommand, TResponse>(TCommand command) 
			where TCommand : ICommand<TResponse>;
	}
}
