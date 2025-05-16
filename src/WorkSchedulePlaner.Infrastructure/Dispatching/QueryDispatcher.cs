using Microsoft.Extensions.DependencyInjection;
using WorkSchedulePlaner.Application.Abstractions.Messaging;

namespace WorkSchedulePlaner.Infrastructure.Dispatching
{
	public class QueryDispatcher : IQueryDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public QueryDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task<TResponse> Dispatch<TQuery, TResponse>(TQuery query) 
			where TQuery : IQuery<TResponse>
		{
			using var scope = _serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery,TResponse>>();

			return await handler.Handle(query);
		}
	}
}
