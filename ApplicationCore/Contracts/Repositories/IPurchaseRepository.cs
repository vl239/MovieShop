using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IPurchaseRepository : IRepository<Purchase>
	{
		Task<int> GetPurchaseCountForMovie(int movieId);

		Task<IEnumerable<Purchase>> GetPurchasesBetween(DateTime start, DateTime end);
	}
}

