namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IUnitOfWork
	{
		public Task SaveAsync();
	}
}
