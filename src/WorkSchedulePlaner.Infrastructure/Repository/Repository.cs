using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Infrastructure.Persistence;

namespace WorkSchedulePlaner.Infrastructure.Repository
{
	public class Repository<T> : IRepository<T>
		where T : class
	{
		protected readonly ApplicationDbContext _dbContext;
		protected readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);

			if (entity is not null) {

				_dbSet.Remove(entity);
			}
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task InsertAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task SaveAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
		}
	}
}
