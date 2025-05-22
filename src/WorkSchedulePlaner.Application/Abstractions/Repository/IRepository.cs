namespace WorkSchedulePlaner.Application.Abstractions.Repository
{
	public interface IRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAsync(Func<T,bool> predicate);
		Task<IEnumerable<T>> GetAllAsync();
		Task InsertAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
