using Microsoft.Extensions.DependencyInjection;
using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Infrastructure.Dispatching
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public CommandDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task Dispatch<TCommand>(TCommand command) 
			where TCommand : ICommand
		{
			using var scope = _serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

			await handler.Handle(command);
		}

		public async Task<TResponse> Dispatch<TCommand, TResponse>(TCommand command) 
			where TCommand : ICommand<TResponse>
		{
			using var scope = _serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand,TResponse>>();

			return await handler.Handle(command);
		}
	}
}
