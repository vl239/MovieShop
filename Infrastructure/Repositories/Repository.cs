using System;
using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly MovieShopDbContext _dbContext;

        public Repository(MovieShopDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

