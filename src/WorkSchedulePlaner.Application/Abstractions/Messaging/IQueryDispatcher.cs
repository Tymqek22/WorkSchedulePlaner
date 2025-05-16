namespace WorkSchedulePlaner.Application.Abstractions.Messaging
{
	public interface IQueryDispatcher
	{
		Task<TResponse> Dispatch<TQuery, TResponse>(TQuery query)
			where TQuery : IQuery<TResponse>;
	}
}
