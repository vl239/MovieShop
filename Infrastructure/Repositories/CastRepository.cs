using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Cast> GetById(int id)
        {
            var castDetails = await _dbContext.Casts
                .Include(c => c.MoviesOfCast)
                .ThenInclude(c => c.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);

            return castDetails;
        }
    }
}

