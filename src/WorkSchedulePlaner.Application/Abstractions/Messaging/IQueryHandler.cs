namespace WorkSchedulePlaner.Application.Abstractions.Messaging
{
	public interface IQueryHandler<in TQuery, TResponse>
		where TQuery : IQuery<TResponse>
	{
		public Task<TResponse> Handle(TQuery query,CancellationToken cancellationToken = default);
	}
}
