using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetPurchaseCountForMovie(int movieId)
        {
            var purchaseCount = await _dbContext.Purchases.Where(p => p.MovieId == movieId).CountAsync();
            return purchaseCount;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesBetween(DateTime start, DateTime end)
        {
            var purchases = await _dbContext.Purchases.Where(p => (p.PurchaseDateTime >= start && p.PurchaseDateTime <= end)).ToListAsync();
            return purchases;
        }
    }
}

